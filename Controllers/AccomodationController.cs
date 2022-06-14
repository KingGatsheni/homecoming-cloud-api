using Azure.Storage.Blobs;
using homecoming.api.Abstraction;
using homecoming.api.Model;
using homecoming.api.Repo;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace homecoming.api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AccomodationController : Controller
    {

        private AccomodationRepo repo;
        private IWebHostEnvironment web;
        private HomecomingDbContext db;
        private readonly BlobServiceClient client;

        public AccomodationController(IWebHostEnvironment host, HomecomingDbContext cx, BlobServiceClient client)
        {
            web = host;
            db = cx;
            this.client = client;
            repo = new AccomodationRepo(web, db, client);
        }


        [HttpGet]
        public IActionResult GetAllAccomodations()
        {
            return Ok(repo.FindAll());
        }

        [HttpPost]
        public IActionResult AddAccomodation([FromForm] Accomodation accomodation)
        {
            if(accomodation != null)
            {
                repo.Add(accomodation);
                return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + accomodation.AccomodationId, accomodation);
            }

            return BadRequest("Room could not be saved!");
        }
        [HttpGet("{id}")]
        public IActionResult GetAccomodation(int id)
        {
            Accomodation accomodation = repo.GetById(id);
            if(accomodation != null)
            {
                return Ok(accomodation);
            }
            else
            {
                return BadRequest("Accomodation with id: "+ id + " Not Found");
            }
        }

        [HttpGet("GetByBusinessId/{id:int}")]
        public IActionResult FindAccomodationByBusinessId(int id)
        {
            List<Accomodation> accomodationList = repo.GetAccomodationByBusinessId(id);
            if(accomodationList != null)
            {
                return Ok(accomodationList);
            }
            else
            {
                return BadRequest("No accomodation found!");
            }
        }

        [HttpGet("GetByAspId/{id}")]
        public IActionResult FindAccomodationsByBusinessUserId(string id)
        {
            List<Accomodation> accomodationList = repo.GetAccomodationsByBusinessUserId(id);
            if (accomodationList != null)
            {
                return Ok(accomodationList);
            }
            else
            {
                return BadRequest("No accomodation found!");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveAccomodation(int id)
        {
            repo.RemoveById(id);
            return Ok("Item deleted");
        }
        [HttpPut("{id}")]
        public IActionResult UpdateAccomodation(int id, Accomodation accom)
        {
            if(accom != null)
            {
                repo.Update(id,accom);
                return Ok("Update Successsful");
            }
            else { return BadRequest("Updated Failed"); }
        }
    }
}

using homecoming.api.Abstraction;
using homecoming.api.Model;
using homecoming.api.Repo;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace homecoming.api.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private HomecomingDbContext db;
        CustomerRepo repo;
        public UserController(HomecomingDbContext db)
        {
            this.db = db;
            repo = new CustomerRepo(db);
        }

        [HttpPost]
        public IActionResult CreateUser(Customer user)
        {
            if(user != null)
            {
                repo.Add(user);
                return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + user.CustomerId, user);
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public IActionResult getUserById(int id)
        {
            var user = repo.GetById(id);
            if(user != null)
            {
                return Ok(user);
            }
            return BadRequest("User Not Found!");
        }

        [HttpGet("getuser/{id}")]
        public IActionResult GetUserByAspUserId(string id)
        {
            var user = repo.GetUserById(id);
            if(user != null)
            {
                return Ok(user);
            }
            return BadRequest("User not Found!");
        }
    }
}

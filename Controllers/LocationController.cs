using homecoming.api.Interfaces;
using homecoming.api.Model;
using homecoming.api.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace homecoming.api.Controllers
{
    [Route("api/[controller]")]
    public class LocationController : Controller
    {
        private readonly HomecomingDbContext db;
        ILocation repo;
        public LocationController(HomecomingDbContext db)
        {
            repo = new LocationRepo(db);
            this.db = db;
        }
        [HttpGet]
        public IActionResult GetLocations()
        {
            var location = repo.GetLocationByName();
            if(location != null)
            {
                return Ok((location));
            }
            return NotFound();
        }

        [HttpGet("GetByLocationId/{id}")]
        public IActionResult GetAccomodationList(string id)
        {
            var accomList = repo.GetAccomodationsByLocationName(id);
            if(accomList != null)
            {
                return Ok(accomList);
            }
            return NotFound();
        }

        [HttpGet("GetAccomByLocationId/{id}")]
        public IActionResult GetAccomodationList(int id)
        {
            var accomList = repo.GetAccomodationsByLoctionId(id);
            if (accomList != null)
            {
                return Ok(accomList);
            }
            return NotFound();
        }
    }
}

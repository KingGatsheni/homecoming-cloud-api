using homecoming.api.Abstraction;
using homecoming.api.Model;
using homecoming.api.Repo;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;


namespace homecoming.api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class RoomController : Controller
    {
        IRepository<Room> repo;
        IWebHostEnvironment web;
        HomecomingDbContext db;

        public RoomController(IWebHostEnvironment host, HomecomingDbContext cx)
        {
            web = host;
            db = cx;
            repo = new RoomRepo(web,db);
        }

        [HttpGet]
        public IActionResult GetAllRooms()
        {
            return Ok(repo.FindAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetRoomById(int id)
        {
            Room room = repo.GetById(id);
           if(room != null)
            {
                return Ok(room);
            }
            else
            {
                return BadRequest("Error no room with id "+ id +" found");
            }
        }

        [HttpPost]
        public IActionResult AddRoom([FromForm] Room room)
        {
            if(room != null)
            {
                repo.Add(room);
                return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + room.RoomId, room);
            }
            return BadRequest(" Room data could not be saved!");
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveRoom(int id)
        {
            //to held errors
            repo.RemoveById(id);
            return Ok("Item with id " + id +" successfully deleted");
        }
        [HttpPut("{id}")]
        public IActionResult UpdateRoomInfo(int id, [FromForm] Room room)
        {
            if (room != null)
            {
                repo.Update(id, room);
                return Ok("Item updated successfully");
            }
            else
            {
                return BadRequest("Uodated Failed");
            }
        }
    }
}

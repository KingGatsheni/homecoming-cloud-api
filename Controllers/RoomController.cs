using Azure.Storage.Blobs;
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
        private RoomRepo repo;
        private IWebHostEnvironment web;
        private HomecomingDbContext db;
        private readonly BlobServiceClient client;

        public RoomController(IWebHostEnvironment host, HomecomingDbContext cx, BlobServiceClient client)
        {
            web = host;
            db = cx;
            this.client = client;
            repo = new RoomRepo(web,db,client);
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
            if (room != null)
            {
                repo.Add(room);

                return Ok(repo.InsertedRoomId);
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

        [HttpGet("getroom/{id}")]
        public IActionResult GetRoomByAccomId(int id)
        {
            Room room = repo.GetRoomByAccomodationId(id);
            if(room != null)
            {
                return Ok(room);
            }
            return NotFound("Error Room not Found!");
        }
        [HttpGet("getrooms/{id}")]
        public IActionResult GetRoomsById(int id)
        {
            var rooms = repo.GetRoomDetailsByRoomId(id);
            if(rooms != null)
            {
                return Ok(rooms);
            }
            return BadRequest("rooms can't be found");
        }
    }
}

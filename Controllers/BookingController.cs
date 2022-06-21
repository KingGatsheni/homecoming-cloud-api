using homecoming.api.Model;
using homecoming.api.Repo;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace homecoming.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : Controller
    {
        private readonly HomecomingDbContext cx;
        private BookingRepo repo;
        public BookingController(HomecomingDbContext cx)
        {
            repo = new BookingRepo(cx);
            this.cx = cx;
        }
        [HttpGet("history/{id}")]
        public IActionResult GetBookingHistory(int id)
        {
            var item = repo.GetBookingByRoomId(id);
            if(item != null)
            {
                return Ok(item);
            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult SendBooking([FromBody]Booking booking)
        {
            if(booking!= null)
            {
                repo.Add(booking);
                return Ok(booking.BookingId);
            }
            return BadRequest();
        }
    }
}

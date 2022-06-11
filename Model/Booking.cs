using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace homecoming.api.Model
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public int NoOfRooms { get; set; }
        public int NoOfOccupants { get; set; }
        public int NoOfDaysBooked { get { return (Check_Out_Date - Check_In_Date).Days; }}
        public Decimal BookingPrice { get; set; }
        public DateTime Check_In_Date { get; set; }
        public DateTime Check_Out_Date { get; set; }
    }
}

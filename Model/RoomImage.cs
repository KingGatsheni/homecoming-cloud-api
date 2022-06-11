using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace homecoming.api.Model
{
    public class RoomImage
    {
        [Key]
        public int MediaId { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public string ImageUrl { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace homecoming.api.Model
{
    public class RoomType
    {
        [Key]
        public int RoomTypeId { get; set; }
        public string Type { get; set; }
        public int NumberOfBeds { get; set; }
        public Boolean Television { get; set; }
        public Boolean Wifi { get; set; }
        public Boolean  Air_condition { get; set; }
        public Boolean Private_bathroom { get; set; }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace homecoming.api.Model
{
    public class Location
    {
        [Key]
        public int LocationId { get; set; }
        public string LocationName { get; set; }
       
    }
}

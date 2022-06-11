using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace homecoming.api.Model
{
    public class ListingImage
    {
        [Key]
        public int ListingImageId { get; set; }
        public int AccomodationId { get; set; }
        public Accomodation Accomodation { get; set; }
        public string ImageUrl { get; set; }
    }
}

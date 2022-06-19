using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace homecoming.api.Model
{
    public class Accomodation
    {
        [Key]
        public int AccomodationId { get; set; }
        public int BusinessId { get; set; }
        public Business Business { get; set; }
        public int LocationId { get; set; }
        public virtual Location GeoLocation { get; set; }
        public string CoverPhoto { get; set; }
        public string AccomodationName { get; set; }
        public string Description { get; set; }
        public decimal Rating { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public virtual List<ListingImage> AccomodationGallary { get; set; }
        public virtual List<Room> AccomodationRooms { get; set; }

        [NotMapped]
        [JsonIgnore]
        public IFormFile CoverImage { get; set; }

        [NotMapped]
        [JsonIgnore]
        public IFormFileCollection ImageList { get; set; }
    }
}

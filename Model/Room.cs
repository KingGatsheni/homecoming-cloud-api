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
    public class Room
    {
      [Key]
        public int RoomId { get; set; }
        public int AccomodationId { get; set; }
        public Accomodation Accomodation { get; set; }
        public string Description { get; set; }
        public Decimal Price { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public virtual List<RoomImage> RoomGallary { get; set; }
        public virtual RoomDetail RoomTypeInfo { get; set; }

        [NotMapped]
        [JsonIgnore]
        public IFormFileCollection ImageList { get; set; }
    }
}

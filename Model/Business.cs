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
    public class Business
    {
        [Key]
        public int BusinessId { get; set; }
        public string AspUser { get; set; }
        public string BusinessName { get; set; }
        public string CoverPhotoUrl { get; set; }
        public string Tel_No { get; set; }
        public string Email { get; set; }
        public string AddressLine1 { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string Province { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedOn { get; set; }

        [NotMapped]
        [JsonIgnore]
        public IFormFile ImageFile { get; set; }
        public virtual List<Accomodation> GetAccomodations { get;}
    }
}

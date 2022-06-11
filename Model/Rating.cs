using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace homecoming.api.Model
{
    public class Rating
    {
        [Key]
        public int RatingId { get; set; }
        public decimal Staff { get; set; }
        public decimal CleanLiness { get; set; }
        public decimal Location { get; set; }
        public decimal Value_for_money { get; set; }
        public decimal Facilities { get; set; }
        public decimal Comfort { get; set; }
        public decimal Free_Wifi { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

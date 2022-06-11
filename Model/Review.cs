using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace homecoming.api.Model
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int BookingId { get; set; }
        public Booking Booking { get; set; }
        public int RatingId { get; set; }
        public Rating Rating { get; set; }
        public string ReviewTitle { get; set; }
        public string positive_comment { get; set; }
        public string negative_comment { get; set; }
        public DateTime ReviewedAt { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace homecoming.api.Model
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string AspUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Cell_No { get; set; }
        public string Email { get; set; }
        public DateTime Dob { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}

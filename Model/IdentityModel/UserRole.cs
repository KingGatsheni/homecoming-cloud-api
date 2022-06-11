using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace homecoming.api.Model.IdentityModel
{
    public class UserRole
    {
        public string Admin { get { return "Admin"; } }
        public string User { get { return "User"; } }
        public string Business { get { return "Business"; } }
    }
}

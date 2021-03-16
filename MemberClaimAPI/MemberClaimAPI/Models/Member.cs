using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberClaimAPI.Models
{
    public class Member
    {
        public int MemberID { get; set; }
        public string EnrollmentDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

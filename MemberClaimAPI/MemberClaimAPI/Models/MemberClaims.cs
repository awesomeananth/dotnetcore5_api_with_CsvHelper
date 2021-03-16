using System.Collections.Generic;

namespace MemberClaimAPI.Models
{
    public class MemberClaims : Member
    {
        public List<Claim> Claims { get; set; }
    }
}

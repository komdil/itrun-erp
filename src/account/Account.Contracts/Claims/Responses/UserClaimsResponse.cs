using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Claims.Responses
{
    public record UserClaimsResponse
    {
        public string Username { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public string Slug { get; set; }
    }
}

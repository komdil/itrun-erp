using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Contracts.Claims.Responses
{
    public record SingleClaimResponse
    {
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public string Slug { get; set; }
    }
}

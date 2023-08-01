using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Api.Contracts.Responses
{
    public record AccountLoginResponse
    {
        public bool Success { get; set; }

        public string AccessToken { get; set; }
    }
}

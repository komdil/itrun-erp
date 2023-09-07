using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Claims.Commands
{
    public class UpdateClaimCommand : IRequest<ApplicationResponse>
    {
        public string ClaimValue { get; set; } = "";
        public string Slug { get; set; } = "";
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Claims.Commands
{
    public class DeleteClaimCommand : IRequest<ApplicationResponse>
    {
        public string Slug { get; set; }
    }
}

using Contracts.Claims.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Claims.Queries
{
    public class GetAllQuery : IRequest<ApplicationResponse<List<UserClaimsResponse>>>
    {
        public string? Username { get; set; } = null;
        public string? ClaimType { get; set; } = null;
    }
}

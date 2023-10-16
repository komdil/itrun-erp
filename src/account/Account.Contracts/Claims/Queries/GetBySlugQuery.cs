using Contracts.Claims.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Claims.Queries
{
    public class GetBySlugQuery : IRequest<ApplicationResponse<UserClaimsResponse>>
    {
        public string Slug { get; set; } = "";
    }
}

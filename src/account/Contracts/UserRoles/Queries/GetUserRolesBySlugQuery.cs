using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.UserRoles.Queries
{
    public class GetUserRolesBySlugQuery : IRequest<ApplicationResponse<UserRolesResponse>>
    {
        public string Slug { get; set; }
    }
}

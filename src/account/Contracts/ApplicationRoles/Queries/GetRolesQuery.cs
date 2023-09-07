using Contracts.ApplicationRoles.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ApplicationRoles.Queries
{
    public class GetRolesQuery : IRequest<ApplicationResponse<List<RoleNameResponse>>>
    {
    }
}

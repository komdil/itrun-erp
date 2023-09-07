using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.UserRoles.Queries
{
    public class GetUserRolesQuery : IRequest<ApplicationResponse<List<UserRolesResponse>>>
    {
        public string Role { get; set; } = null;
        public string UserName { get; set; } = null;


    }
}

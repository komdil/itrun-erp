using Account.Contracts.UserRoles.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Contracts.UserRoles.Queries
{
    public class GetUserRolesQuery : IRequest<List<UserRolesResponse>>
    {
        public string Role { get; set; }
        public string UserName { get; set; }
    }
}

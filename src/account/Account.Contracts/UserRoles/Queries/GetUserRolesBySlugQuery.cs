using Account.Contracts.UserRoles.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Contracts.UserRoles.Queries
{
    public class GetUserRoleQuery : IRequest<UserRolesResponse>
    {
        public string Slug { get; set; }
    }
}

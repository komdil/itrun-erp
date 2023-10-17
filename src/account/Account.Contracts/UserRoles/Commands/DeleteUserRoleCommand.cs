using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Contracts.UserRoles.Commands
{
    public class DeleteUserRoleCommand : IRequest
    {
        public string Slug { get; set; }
    }
}

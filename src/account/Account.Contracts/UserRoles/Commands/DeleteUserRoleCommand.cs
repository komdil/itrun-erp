using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.UserRoles.Commands
{
    public class DeleteUserRoleCommand : IRequest<ApplicationResponse<bool>>
    {
        public string Slug { get; set; }
    }
}

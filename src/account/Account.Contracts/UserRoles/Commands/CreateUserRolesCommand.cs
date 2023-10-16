using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.UserRoles.Commands
{
    public class CreateUserRolesCommand : IRequest<ApplicationResponse<string>>
    {
        public Guid RoleId { get; set; }
        public Guid UserId { get; set; }

    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ApplicationRoles.Commands
{
    public class CreateRoleCommand : IRequest<ApplicationResponse<Guid>>
    {
        public string RoleName { get; set; }
    }
}

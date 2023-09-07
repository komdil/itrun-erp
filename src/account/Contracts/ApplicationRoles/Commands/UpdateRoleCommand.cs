using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ApplicationRoles.Commands
{
    public class UpdateRoleCommand : IRequest<ApplicationResponse<Guid>>
    {
        public string Slug { get; set; }
        public string NewRoleName { get; set; }
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Requests.Admin
{
    public class UpdateRoleCommand : IRequest<bool>
    {
        public string Role { get; set; }

        public Guid UserId { get; set; }

        public Guid RoleId { get; set; }
    }
}

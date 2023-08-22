using Contracts.Requests.Admin;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Admin
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, bool>
    {
        public async Task<bool> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            //UPDATE ROLE
            return true;
        }
    }
}

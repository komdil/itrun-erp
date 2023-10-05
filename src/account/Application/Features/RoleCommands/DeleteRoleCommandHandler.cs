using Application.Contract.ApplicationRoles.Commands;
using Contracts;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.RoleCommands
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, ApplicationResponse<bool>>
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public DeleteRoleCommandHandler(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<ApplicationResponse<bool>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {

            var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Slug == request.Slug);
            if (role == null)
            {
                return new ApplicationResponseBuilder<bool>().SetErrorMessage("role not found").Success(false).Build();
            }

            await _roleManager.DeleteAsync(role);
            return new ApplicationResponseBuilder<bool>().Success(true).Build();

        }
    }
}

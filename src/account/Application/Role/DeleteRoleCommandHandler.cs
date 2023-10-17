using Application.Common.Exceptions;
using Application.Contract.ApplicationRoles.Commands;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Role
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand>
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public DeleteRoleCommandHandler(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Name == request.Name);
            if (role == null)
                throw new NotFoundException();

            var result = await _roleManager.DeleteAsync(role);
            if (!result.Succeeded)
                throw new ValidationFailedException(result.Errors);
        }
    }
}

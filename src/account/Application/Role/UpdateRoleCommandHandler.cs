using Application.Common.Exceptions;
using Application.Contract.ApplicationRoles.Commands;
using Application.Contract.ApplicationRoles.Responses;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Role
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, RoleNameResponse>
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IMapper _mapper;
        public UpdateRoleCommandHandler(RoleManager<ApplicationRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<RoleNameResponse> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Name == request.Name, cancellationToken);
            if (role == null)
                throw new NotFoundException();

            role.Name = request.NewRoleName;
            var result = await _roleManager.UpdateAsync(role);
            if (!result.Succeeded)
                throw new ValidationFailedException(result.Errors);
            return _mapper.Map<RoleNameResponse>(role);
        }
    }
}


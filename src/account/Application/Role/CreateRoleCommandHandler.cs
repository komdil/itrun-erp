using Application.Contract.ApplicationRoles.Commands;
using Domain;
using MediatR;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Application.Contract.ApplicationRoles.Responses;
using Application.Common.Exceptions;

namespace Application.Role
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, RoleNameResponse>
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IMapper _mapper;

        public CreateRoleCommandHandler(RoleManager<ApplicationRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<RoleNameResponse> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = _mapper.Map<ApplicationRole>(request);
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
                return _mapper.Map<RoleNameResponse>(role);
            else
                throw new ValidationFailedException(result.Errors);
        }
    }
}

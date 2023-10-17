using Application.Common.Exceptions;
using Application.Contract.ApplicationRoles.Queries;
using Application.Contract.ApplicationRoles.Responses;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Role
{
    public class GetSingleRoleQueryHandler : IRequestHandler<GetSingleRoleQuery, RoleNameResponse>
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IMapper _mapper;

        public GetSingleRoleQueryHandler(RoleManager<ApplicationRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }
        public async Task<RoleNameResponse> Handle(GetSingleRoleQuery request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Name == request.Name, cancellationToken);
            if (role == null)
                throw new NotFoundException();

            return _mapper.Map<RoleNameResponse>(role);
        }
    }
}

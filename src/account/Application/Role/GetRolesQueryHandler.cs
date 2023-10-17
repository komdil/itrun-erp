using Application.Contract.ApplicationRoles.Queries;
using Application.Contract.ApplicationRoles.Responses;
using MediatR;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Domain;
using Microsoft.EntityFrameworkCore;
using Application.Common;

namespace Application.Role
{
    public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, List<RoleNameResponse>>
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IMapper _mapper;

        public GetRolesQueryHandler(RoleManager<ApplicationRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public Task<List<RoleNameResponse>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            return _mapper.ProjectTo<RoleNameResponse>(_roleManager.Roles.Where(s => s.Name != Constants.SuperAdminRoleName)).ToListAsync(cancellationToken);
        }
    }
}

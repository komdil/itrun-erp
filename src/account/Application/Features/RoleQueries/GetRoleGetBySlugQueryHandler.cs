using Application.Contract.ApplicationRoles.Queries;
using Application.Contract.ApplicationRoles.Responses;
using AutoMapper;
using Contracts;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.RoleQueries
{
    public class GetRoleGetBySlugQueryHandler : IRequestHandler<GetRoleBySlugQuery, ApplicationResponse<RoleNameResponse>>
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IMapper _mapper;

        public GetRoleGetBySlugQueryHandler(RoleManager<ApplicationRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }
        public async Task<ApplicationResponse<RoleNameResponse>> Handle(GetRoleBySlugQuery request, CancellationToken cancellationToken)
        {

            var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Slug == request.Slug);

            var roleResponses = _mapper.Map<RoleNameResponse>(role);

            var response = new ApplicationResponseBuilder<RoleNameResponse>()
                .SetResponse(roleResponses)
                .Success(true)
                .Build();

            return response;
        }
    }
}

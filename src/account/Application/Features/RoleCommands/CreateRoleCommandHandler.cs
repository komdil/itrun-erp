using Application.Contract.ApplicationRoles.Commands;
using Contracts;
using Domain;
using MediatR;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.RoleCommands
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, ApplicationResponse<Guid>>
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IMapper _mapper;

        public CreateRoleCommandHandler(RoleManager<ApplicationRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<ApplicationResponse<Guid>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var role = _mapper.Map<ApplicationRole>(request);

                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return new ApplicationResponse<Guid> { Response = role.Id };
                }

                return new ApplicationResponse<Guid> { Success = false, ErrorMessage = result.Errors.First().Description };
            }
            catch (Exception e)
            {
                return new ApplicationResponse<Guid> { Success = false, ErrorMessage = e.Message };
            }
        }
    }
}

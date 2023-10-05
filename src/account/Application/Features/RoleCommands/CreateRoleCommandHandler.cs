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
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, ApplicationResponse<Guid>>
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public CreateRoleCommandHandler(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<ApplicationResponse<Guid>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var role = new ApplicationRole
                {
                    Id = Guid.NewGuid(),
                    Name = request.RoleName,
                    NormalizedName = request.RoleName.ToLower(),
                    Slug = request.RoleName.ToLower(),
                };

                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return new ApplicationResponseBuilder<Guid>().SetResponse(role.Id).Build();
                }

                return new ApplicationResponseBuilder<Guid>().SetErrorMessage(result.Errors.First().Description)
                    .Success(false).Build();
            }
            catch (Exception e)
            {
                return new ApplicationResponseBuilder<Guid>().Success(false).SetException(e).Build();
            }
        }
    }
}

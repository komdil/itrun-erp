using Contracts.UserRoles.Queries;
using Contracts.UserRoles.Responses;
using Contracts;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserRoleQueris
{
    public class GetUserRolesQueryHandler : IRequestHandler<GetUserRolesQuery, ApplicationResponse<List<UserRolesResponse>>>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public GetUserRolesQueryHandler(UserManager<ApplicationUser> userManager, IApplicationDbContext context, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
        }

        private readonly IApplicationDbContext _context;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public async Task<ApplicationResponse<List<UserRolesResponse>>> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
        {


            if (request.UserName != null)
            {
                var user = _userManager.Users.FirstOrDefault(s => s.UserName.Contains(request.UserName));
                if (user == null)
                    return new ApplicationResponseBuilder<List<UserRolesResponse>>().SetErrorMessage("User not found").Success(false).Build();


                var userRoles = await _userManager.GetRolesAsync(user);

                if (request.Role != null)
                {
                    userRoles = userRoles.Where(s => s.Contains(request.Role)).ToList();
                }

                return new ApplicationResponseBuilder<List<UserRolesResponse>>()
                    .SetResponse((await Task.WhenAll(userRoles.Select(async s =>
                        new UserRolesResponse
                        {
                            RoleName = s,
                            UserName = user.UserName,
                            Slug = $"{user.UserName}-{(await _roleManager.FindByNameAsync(s)).Slug}"
                        }))).ToList());
            }
            if (request.Role != null)
            {
                var role = await _context.Roles.FirstOrDefaultAsync(s => s.NormalizedName.Contains(request.Role));
                if (role == null)
                    return new ApplicationResponseBuilder<List<UserRolesResponse>>().SetErrorMessage("role not found").Success(false).Build();
                var userRoles = await _context.UserRoles.Where(s => s.RoleId == role.Id).ToListAsync();
                var responses = new List<UserRolesResponse>();
                foreach (var userRole in userRoles)
                {
                    var response = new UserRolesResponse()
                    {
                        RoleName = role.Name,
                        UserName = (await _userManager.FindByIdAsync(userRole.UserId.ToString())).UserName,
                        Slug = $"{(await _userManager.FindByIdAsync(userRole.UserId.ToString())).UserName}-{role.Slug}"

                    };
                    responses.Add(response);
                }
                return new ApplicationResponseBuilder<List<UserRolesResponse>>().SetResponse(responses).Build();
            }
            var users = await _userManager.Users.ToListAsync();


            var res = await _context.UserRoles.ToListAsync();


            return new ApplicationResponseBuilder<List<UserRolesResponse>>().
                SetResponse(res.Select(s => new UserRolesResponse
                {
                    UserName = (_userManager.Users.FirstOrDefault(u => u.Id == s.UserId)).UserName,
                    RoleName = (_roleManager.Roles.FirstOrDefault(r => r.Id == s.RoleId)).Name,
                    Slug = s.Slug
                }).ToList()).Success(true).Build();

        }
    }
}

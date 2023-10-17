using Application.Abstractions.Data;
using Application.Common;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    internal class ApplicationDbInitializer : IApplicationDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public ApplicationDbInitializer(ApplicationDbContext context, RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public void Initialize()
        {
            if (_context.Database.IsRelational())
            {
                _context.Database.Migrate();
            }

            var role = _roleManager.Roles.FirstOrDefault(r => r.Name == Constants.SuperAdminRoleName);
            if (role == null)
            {
                role = new ApplicationRole()
                {
                    Name = Constants.SuperAdminRoleName
                };
                var result = _roleManager.CreateAsync(role).Result;
                AssertIdentitySuccess(result);
            }

            var superAdmin = _userManager.Users.FirstOrDefault(s => s.UserName == Constants.SuperAdminUserName);
            if (superAdmin == null)
            {
                superAdmin = new ApplicationUser()
                {
                    UserName = Constants.SuperAdminUserName,
                };
                var result = _userManager.CreateAsync(superAdmin, Constants.SuperAdminDefaultPassword).Result;
                AssertIdentitySuccess(result);
            }

            bool roleAssigned = _userManager.IsInRoleAsync(superAdmin, role.Name).Result;
            if (!roleAssigned)
            {
                var result = _userManager.AddToRoleAsync(superAdmin, role.Name).Result;
                AssertIdentitySuccess(result);
            }
        }

        private void AssertIdentitySuccess(IdentityResult identityResult)
        {
            if (!identityResult.Succeeded)
            {
                throw new InvalidOperationException(string.Join(',', identityResult.Errors.Select(s => s.Description)));
            }
        }
    }
}

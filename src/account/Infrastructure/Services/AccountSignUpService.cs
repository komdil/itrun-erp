using Application.Abstractions.Services;
using Contracts.Requests.Auth;
using Contracts.Response.Auth;
using Domain;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services
{
    public class AccountSignUpService : IAccountSignUpService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public AccountSignUpService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<AccountSignUpResponse> SignUpAsync(AccountSignUpRequest model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return new AccountSignUpResponse()
                {
                    Success = false,
                    Message = "User already exists!"
                };

            Organization organization = new Organization()
            {
                Name = model.Username,
            };

            Contact contact = new Contact()
            {
                Id = Guid.NewGuid(),
            };

            ApplicationUser user = new ApplicationUser()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                Organization = organization,
                PrimaryContact = contact,
            };
            organization.Owner = user;

            // Save to DB new organization

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return new AccountSignUpResponse() { Success = false, Message = "User creation failed! Please check user details and try again." };

            return new AccountSignUpResponse()
            {
                Success = true,
                Message = "User successfully created"
            };
        }
    }
}

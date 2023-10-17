using Application.Abstractions.Services;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Account.Contracts.Auth;

namespace Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;

        public AccountService(UserManager<ApplicationUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<AccountSignInResponse> SignInAsync(AccountSignInRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user != null &&
                await _userManager.CheckPasswordAsync(user, request.Password))
            {
                var token = await _tokenService.GenerateTokenAsync(user);
                return new AccountSignInResponse()
                {
                    Success = true,
                    Token = token,
                };
            }

            return new AccountSignInResponse() { Success = false, };
        }

        public async Task<AccountSignUpResponse> SignUpAsync(AccountSignUpRequest request)
        {
            var userExists = await _userManager.FindByNameAsync(request.Username);
            if (userExists != null)
                return new AccountSignUpResponse()
                {
                    Success = false,
                    Message = "User already exists!"
                };

            Organization organization = new Organization()
            {
                Name = request.Username,
            };

            Contact contact = new Contact()
            {
                Id = Guid.NewGuid(),
            };

            ApplicationUser user = new ApplicationUser()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = request.Username,
                Organization = organization,
                PrimaryContact = contact,
            };
            organization.Owner = user;

            // Save to DB new organization

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                return new AccountSignUpResponse() { Success = false, Message = "User creation failed! Please check user details and try again." };

            var token = await _tokenService.GenerateTokenAsync(user);

            return new AccountSignUpResponse()
            {
                Success = true,
                Message = "User successfully created",
                Token = token
            };
        }
    }
}

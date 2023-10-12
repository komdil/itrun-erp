using Application.Abstractions.Services;
using Account.Contracts.Requests.Auth;
using Account.Contracts.Response.Auth;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AccountService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<AccountSignInResponse> SignInAsync(AccountSignInRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user != null &&
                await _userManager.CheckPasswordAsync(user, request.Password))
            {
                var token = await GenerateJwtTokenForUser(user);
                return new AccountSignInResponse()
                {
                    Success = true,
                    Token = token,
                };
            }

            return new AccountSignInResponse() { Success = false, };
        }

        private async Task<string> GenerateJwtTokenForUser(ApplicationUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.PrimarySid, user.Id.ToString()),
                };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(5),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
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

            var token = await GenerateJwtTokenForUser(user);

            return new AccountSignUpResponse()
            {
                Success = true,
                Message = "User successfully created",
                Token = token
            };
        }
    }
}

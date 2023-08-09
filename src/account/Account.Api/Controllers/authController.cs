using Application.Abstractions.Services;
using Contracts.Requests.Auth;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Account.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class authController : ControllerBase
    {
        private IAccountSignUpService _accountSignUpService;
        private IAccountSignInService _accountSignInService;

        public authController(IAccountSignUpService accountSignUpService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager, IConfiguration configuration, IAccountSignInService accountSignInService)
        {
            _accountSignUpService = accountSignUpService;
            _accountSignInService = accountSignInService;
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SingUp([FromBody] AccountSignUpRequest model)
        {
            var response = await _accountSignUpService.SignUpAsync(model);
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }

            //var userExists = await _userManager.FindByNameAsync(model.Username);
            //if (userExists != null)
            //    return StatusCode(StatusCodes.Status500InternalServerError,
            //        new { Status = "Error", Message = "User already exists!" });

            //Organization organization = new Organization()
            //{
            //    Name = model.Username,
            //};

            //Contact contact = new Contact()
            //{
            //    Id = Guid.NewGuid(),
            //};

            //ApplicationUser user = new ApplicationUser()
            //{
            //    SecurityStamp = Guid.NewGuid().ToString(),
            //    UserName = model.Username,
            //    Organization = organization,
            //    PrimaryContact = contact,
            //};
            //organization.Owner = user;

            //// Save to DB new organization

            //var result = await _userManager.CreateAsync(user, model.Password);
            //if (!result.Succeeded)
            //    return StatusCode(StatusCodes.Status500InternalServerError,
            //        new { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            //AccountSigninRequest accountLogin = new AccountSigninRequest()
            //{
            //    Username = user.UserName,
            //    Password = model.Password
            //};

            //var signInResult = await SignIn(accountLogin);
            //return Ok(signInResult);
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] AccountSigninRequest model)
        {
            var response = await _accountSignInService.SignInAsync(model);
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return Unauthorized();
            }
            //var user = await _userManager.FindByNameAsync(model.Username);
            //if (user != null &&
            //    await _userManager.CheckPasswordAsync(user, model.Password))
            //{
            //    var userRoles = await _userManager.GetRolesAsync(user);

            //    var authClaims = new List<Claim>
            //    {
            //        new Claim(ClaimTypes.Name, user.UserName),
            //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            //    };

            //    foreach (var userRole in userRoles)
            //    {
            //        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            //    }

            //    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            //    var token = new JwtSecurityToken(
            //        issuer: _configuration["JWT:ValidIssuer"],
            //        audience: _configuration["JWT:ValidAudience"],
            //        expires: DateTime.Now.AddHours(5),
            //        claims: authClaims,
            //        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            //        );

            //    return Ok(new AccountSigninResponse
            //    {
            //        Token = new JwtSecurityTokenHandler().WriteToken(token),
            //    });
            //}
            //return Unauthorized();
        }
    }
}

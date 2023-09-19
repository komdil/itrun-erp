using Application.Abstractions.Services;
using Account.Contracts.Requests.Auth;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Account.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private IAccountSignUpService _accountSignUpService;
        private IAccountSignInService _accountSignInService;

        public AuthController(IAccountSignUpService accountSignUpService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager, IConfiguration configuration, IAccountSignInService accountSignInService)
        {
            _accountSignUpService = accountSignUpService;
            _accountSignInService = accountSignInService;
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] AccountSignUpRequest model)
        {
            var response = await _accountSignUpService.SignUpAsync(model);
            if (response.Success)
            {
                AccountSignInRequest accountSignInRequest = new AccountSignInRequest()
                {
                    Username= model.Username,
                    Password= model.Password
                };

                var signInResult = await SignIn(accountSignInRequest);
                return Ok(signInResult);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] AccountSignInRequest model)
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
        }
    }
}

using Application.Abstractions.Services;
using Account.Contracts.Requests.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Account.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private IAccountService _accountService;

        public AuthController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] AccountSignUpRequest accountSignInRequest)
        {
            var response = await _accountService.SignUpAsync(accountSignInRequest);
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] AccountSignInRequest model)
        {
            var response = await _accountService.SignInAsync(model);
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

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
        private IConfiguration _configuration;

        public AuthController(IAccountService accountService, IConfiguration configuration)
        {
            _accountService = accountService;
            _configuration = configuration;
        }

        [HttpGet("GetSome/{name}")]
        public IActionResult GetSome(string name)
        {
            return Ok(_configuration[name]);
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

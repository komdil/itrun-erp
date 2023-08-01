using Account.Api.Contracts.Requests;
using Account.Api.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Account.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost("sign-in")]
        public IActionResult SignIn([FromBody] AccountLoginRequest accountLoginRequest)
        {
            return Ok(new AccountLoginResponse() { Success = true, AccessToken = "abc" });
        }
    }
}

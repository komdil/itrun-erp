﻿using Application.Abstractions.Services;
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
        }
    }
}

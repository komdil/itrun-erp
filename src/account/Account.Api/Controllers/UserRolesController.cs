using Contracts.UserRoles.Commands;
using Contracts.UserRoles.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Account.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRolesController : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> Get(string role = null, string userName = null)
        {
            var result = await Sender.
                .Send(new GetUserRolesQuery
                {
                    Role = role,
                    UserName = userName
                });
            if (!result.IsSuccess)
                return BadRequest();       
            return Ok(result.Response);
        }

        [HttpGet("{slug}")]
        public async Task<IActionResult> Get(string slug)
        {
            var query = new GetUserRolesBySlugQuery { Slug = slug };
            var result = await _mediator.Send(query);
            if (!result.IsSuccess)
                return BadRequest();             
            return Ok(result.Response);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserRolesCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                throw new ValidationFailedException(result.ErrorMessage);
            return Ok(result.Response);
     
        }
        [HttpDelete("{slug}")]
        public async Task<IActionResult> Delete(string slug)
        {
            var command = new DeleteUserRoleCommand { Slug = slug };
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                return BadRequest(result.ErrorMessage);
            return Ok(result);
        }
    }
}

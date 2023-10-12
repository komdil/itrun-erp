
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Contracts.UserRoles.Commands;
using Application.Contract.ApplicationRoles.Queries;
using Application.Contract.ApplicationRoles.Commands;

namespace Account.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ApiBaseController
    {

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetRolesQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet("{slug}")]
        public async Task<IActionResult> Get(string slug)
        {
            var query = new GetRoleBySlugQuery { Slug = slug };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserRolesCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                throw new ValidationFailedException(result.ErrorMessage);
            return Ok(result);
        }
            
        [HttpPut("{slug}")]
        public async Task<IActionResult> Update(UpdateRoleCommand command, string slug)
        {
            command.Slug = slug;
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                throw new NotFoundException();
            return Ok(result);
        }

        [HttpDelete("{slug}")]
        public async Task<IActionResult> Delete(string slug)
        {
            var command = new DeleteRoleCommand { Slug = slug };
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                throw new NotFoundException();

            return Ok(result);
        }
    }
}

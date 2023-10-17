using Microsoft.AspNetCore.Mvc;
using Application.Contract.ApplicationRoles.Queries;
using Application.Contract.ApplicationRoles.Commands;
using Application.Contract.ApplicationRoles.Responses;
using Microsoft.AspNetCore.Authorization;
using Application.Common;

namespace Account.Api.Controllers
{
    [Authorize(Constants.SuperAdminPolicy)]
    public class RolesController : ApiControllerBase
    {
        [HttpGet]
        public async Task<List<RoleNameResponse>> Get([FromQuery] GetRolesQuery query)
        {
            return await Sender.Send(query);
        }

        [HttpGet("{slug}")]
        public async Task<RoleNameResponse> Get(string slug)
        {
            var query = new GetSingleRoleQuery { Name = slug };
            return await Sender.Send(query);
        }

        [HttpPost]
        public async Task<RoleNameResponse> Create(CreateRoleCommand command)
        {
            return await Sender.Send(command);
        }

        [HttpPut("{slug}")]
        public async Task<RoleNameResponse> Update(UpdateRoleCommand command, string slug)
        {
            command.Name = slug;
            return await Sender.Send(command);
        }

        [HttpDelete("{slug}")]
        public async Task<IActionResult> Delete(string slug)
        {
            var command = new DeleteRoleCommand { Name = slug };
            await Sender.Send(command);
            return NoContent();
        }
    }
}

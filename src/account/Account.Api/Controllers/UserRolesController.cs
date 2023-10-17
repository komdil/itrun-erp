using Application.Common;
using Account.Contracts.UserRoles.Commands;
using Account.Contracts.UserRoles.Queries;
using Account.Contracts.UserRoles.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Account.Api.Controllers
{
    [Authorize(Constants.SuperAdminPolicy)]
    public class UserRolesController : ApiControllerBase
    {
        [HttpGet]
        public async Task<List<UserRolesResponse>> Get([FromQuery] GetUserRolesQuery getUserRolesQuery)
        {
            return await Sender.Send(getUserRolesQuery);
        }

        [HttpGet("{slug}")]
        public async Task<UserRolesResponse> Get(string slug)
        {
            var query = new GetUserRoleQuery { Slug = slug };
            return await Sender.Send(query);
        }

        [HttpPost]
        public async Task<UserRolesResponse> Create(CreateUserRolesCommand command)
        {
            return await Sender.Send(command);
        }

        [HttpDelete("{slug}")]
        public async Task<IActionResult> Delete(string slug)
        {
            var command = new DeleteUserRoleCommand { Slug = slug };
            await Sender.Send(command);
            return NoContent();
        }
    }
}

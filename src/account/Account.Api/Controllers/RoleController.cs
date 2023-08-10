using Application.Admin;
using Contracts.Requests.Admin;
using Microsoft.AspNetCore.Mvc;

namespace Account.Api.Controllers
{
    public class RoleController : AccountBaseController
    {
        [HttpPost]
        public async Task<bool> Post([FromBody] AssignRoleCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}

using Application.Common;
using Account.Contracts.Claims.Commands;
using Account.Contracts.Claims.Queries;
using Account.Contracts.Claims.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Account.Api.Controllers
{
    [Authorize(Constants.SuperAdminPolicy)]
    public class UserClaimsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<List<SingleClaimResponse>> Get([FromQuery] GetUserClaimsQuery claimsQuery)
        {
            return await Sender.Send(claimsQuery);
        }

        [HttpPost]
        public async Task<SingleClaimResponse> Create(CreateUserClaimCommand command)
        {
            return await Sender.Send(command);
        }

        [HttpDelete("{slug}")]
        public async Task<IActionResult> Delete(string slug)
        {
            var command = new DeleteUserClaimCommand { Slug = slug };
            await Sender.Send(command);
            return NoContent();
        }
    }
}

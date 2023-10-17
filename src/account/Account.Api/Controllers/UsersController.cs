using Application.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Account.Contracts.User.Queries;
using Account.Contracts.User.Response;

namespace Account.Api.Controllers
{
    [Authorize(Constants.SuperAdminPolicy)]
    public class UsersController : ApiControllerBase
    {
        [HttpGet]
        public async Task<List<UserResponse>> Get([FromQuery] GetUsersQuery query)
        {
            return await Sender.Send(query);
        }
    }
}

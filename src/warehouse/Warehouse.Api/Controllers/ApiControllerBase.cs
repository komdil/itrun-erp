using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Warehouse.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
        private ISender _mediatR;
        protected ISender Sender => _mediatR ??= HttpContext.RequestServices.GetService<ISender>();
    }
}

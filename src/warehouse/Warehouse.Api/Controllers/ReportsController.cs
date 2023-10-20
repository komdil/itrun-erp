using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Contracts.Reports;

namespace Warehouse.Api.Controllers
{
    [Authorize]
    public class ReportsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ProductAggregatesResponse> Get([FromQuery] GetProductAggregatesQuery request)
        {
            return await Sender.Send(request);
        }
    }
}

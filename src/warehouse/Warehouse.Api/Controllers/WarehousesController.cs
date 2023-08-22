using Contracts.Warehouse;
using Microsoft.AspNetCore.Mvc;

namespace Warehouse.Api.Controllers
{
    public class WarehousesController : ApiControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateWarehouseRequest request)
        {
            var response = await Sender.Send(request);
            return Created($"warehouses/{response.Slug}", response);
        }
    }
}

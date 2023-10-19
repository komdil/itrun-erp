using Warehouse.Contracts.Warehouse;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Warehouse.Api.Utilities;

namespace Warehouse.Api.Controllers
{
    [Authorize]
    public class WarehousesController : ApiControllerBase
    {
        [HttpGet]
        public async Task<List<SingleWarehouseResponse>> Get([FromQuery] GetWarehousesQuery query)
        {
            return await Sender.Send(query);
        }

        [HttpGet("{id}")]
        public async Task<SingleWarehouseResponse> Get(Guid id)
        {
            var query = new GetSingleWarehouseQuery()
            {
                Id = id
            };
            return await Sender.Send(query);
        }

        [Authorize(Roles = $"{Constants.SuperAdminRoleName},{Constants.BuyerRoleName}")]
        [HttpPut("{id}")]
        public async Task<SingleWarehouseResponse> Put(Guid id, [FromBody] UpdateWarehouseRequest updateWarehouseRequest)
        {
            updateWarehouseRequest.Id = id;
            return await Sender.Send(updateWarehouseRequest);
        }

        [Authorize(Roles = $"{Constants.SuperAdminRoleName},{Constants.BuyerRoleName}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleteRequest = new DeleteWarehouseRequest()
            {
                Id = id
            };
            await Sender.Send(deleteRequest);
            return NoContent();
        }

        [Authorize(Roles = $"{Constants.SuperAdminRoleName},{Constants.BuyerRoleName}")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateWarehouseRequest request)
        {
            var response = await Sender.Send(request);
            return Created($"warehouses/{response.Slug}", response);
        }
    }
}

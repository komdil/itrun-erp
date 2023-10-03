using Warehouse.Contracts.Warehouse;
using Microsoft.AspNetCore.Mvc;

namespace Warehouse.Api.Controllers
{
    public class WarehousesController : ApiControllerBase
    {
        [HttpGet]
        public async Task<List<SingleWarehouseResponse>> Get([FromQuery] GetWarehouseQuery query)
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

        [HttpPut("{id}")]
        public async Task<SingleWarehouseResponse> Put(Guid id, [FromBody] UpdateWarehouseRequest updateWarehouseRequest)
        {
            updateWarehouseRequest.Id = id;
            return await Sender.Send(updateWarehouseRequest);
        }

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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateWarehouseRequest request)
        {
            var response = await Sender.Send(request);
            return Created($"warehouses/{response.Slug}", response);
        }
    }
}

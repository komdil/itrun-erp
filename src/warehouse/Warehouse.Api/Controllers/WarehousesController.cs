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
        [HttpGet]
        public async Task<IActionResult> Get(GetWarehouse getwarehouse)
        {
            return new() getwarehouse;
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteWarehouse deleteWarehouse)
        {
            bool success = true;
            var delete1 = deleteWarehouse.Get(id);
            if (delete1 != null)
            {
                deleteWarehouse.Delete(delete1.id);
            }
            else
            {
                success = false;
            }
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] PutWarehouse putWarehouse)
        {
            bool success = true;
            var put = putWarehouse.Put(id);
            if (put != null)
            {
                putWarehouse.Update(put.id);
            }
            else
            {
                success = false;
            }
        }

    }

}
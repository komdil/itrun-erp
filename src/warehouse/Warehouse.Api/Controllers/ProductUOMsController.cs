using Warehouse.Contracts.ProductUOM;
using Microsoft.AspNetCore.Mvc;

namespace Warehouse.Api.Controllers
{
    public class ProductUOMsController : ApiControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProductUOMRequest request)
        {
            var response = await Sender.Send(request);
            return Created($"productuoms/{response.Slug}", response);
        }

    }
}

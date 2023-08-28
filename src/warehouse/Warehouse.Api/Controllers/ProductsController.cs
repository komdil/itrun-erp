using Contracts.Product;
using Contracts.ProductUOM;
using Microsoft.AspNetCore.Mvc;

namespace Warehouse.Api.Controllers
{
    public class ProductsController : ApiControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProductRequest request)
        {
            var response = await Sender.Send(request);
            return Created($"productuoms/{response}", response);
        }
    }
}

using Contracts.Product;
using Microsoft.AspNetCore.Mvc;

namespace Warehouse.Api.Controllers
{
    public class ProductsController : ApiControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProductRequest request)
        {
            var response = await Sender.Send(request);
            return Created($"products/{response}", response);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateProductRequest request)
        {
            await Sender.Send(request);
            return NoContent();
        }

        [HttpDelete("{slug}")]
        public async Task<IActionResult> Delete(string slug)
        {
            var request = new DeleteProductRequest(slug);
            await Sender.Send(request);
            return NoContent();
        }
    }
}

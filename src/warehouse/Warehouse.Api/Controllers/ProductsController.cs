using Warehouse.Contracts.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Warehouse.Api.Controllers
{
    [Authorize]
    public class ProductsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<List<SingleProductResponse>> Get([FromQuery] GetProductsQuery request)
        {
            return await Sender.Send(request);
        }

        [HttpGet("{id}")]
        public async Task<SingleProductResponse> Get(Guid id)
        {
            var query = new GetSingleProductQuery()
            {
                ProductId = id
            };
            return await Sender.Send(query);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProductRequest request)
        {
            var response = await Sender.Send(request);
            return Created($"products/{response.Id}", response);
        }

        [HttpPut("{id}")]
        public async Task<SingleProductResponse> Put(Guid id, [FromBody] UpdateProductRequest request)
        {
            request.Id = id;
            return await Sender.Send(request);
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

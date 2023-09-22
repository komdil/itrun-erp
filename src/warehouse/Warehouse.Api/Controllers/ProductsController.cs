using Warehouse.Contracts.Product;
using Microsoft.AspNetCore.Mvc;

namespace Warehouse.Api.Controllers
{
    public class ProductsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<ProductListResponse>> Get([FromQuery] GetProductListQueryRequest request)
        {
            var query = new GetProductListQueryRequest
            {
                Category = request.Category,
                Manufacturer = request.Manufacturer
            };

            var queryResult = await Sender.Send(query);
            return Ok(queryResult);
        }

        [HttpGet("id")]
        public async Task<ActionResult<GetProductDetailsQueryResponse>> Get(Guid id)
        {
            var query = new GetProductDetailsQueryRequest()
            {
                ProductId = id
            };

            var queryResult = await Sender.Send(query);
            return Ok(queryResult);
        }

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

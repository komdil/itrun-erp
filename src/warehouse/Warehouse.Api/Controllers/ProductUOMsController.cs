using Warehouse.Contracts.ProductUOM;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Contracts.Product;

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
        [HttpGet]
        public async Task<List<SingleProductUomResponse>> Get([FromQuery] GetProductsUomQuery getproductuom)
        {
            return await Sender.Send(getproductuom);
        }

        [HttpGet("{id}")]
        public async Task<SingleProductUomResponse> Get(Guid id)
        {
            var query = new GetSingleProductUomQuery()
            {
                ProductUomId = id
            };
            return await Sender.Send(query);
        }

        [HttpDelete("{slug}")]
        public async Task<IActionResult> Delete(string slug)
        {
            var request = new DeleteProductUomRequest(slug);
            await Sender.Send(request);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<SingleProductUomResponse> Put(Guid id, [FromBody] UpdateProductUomRequest putproduct)
        {
            putproduct.Id = id;
            return await Sender.Send(putproduct);
        }
    }
}

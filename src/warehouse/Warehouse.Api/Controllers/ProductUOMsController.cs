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
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]GetProductUomQuery getproductuom)
        {
            return await Sender.send(getproductuom);
        }

        [HttpGet("id")]
        public async Task<IActionResult> Get(Guid id)
        {
            var response = Get(id);
            {
                ProductuomId = id
            };
            return await ProductuomId(id);
        }

        [HttpDelete("{slug}")]
        public async Task<IActionResult> Delete([FromBody] string slug)
        {
            var request = new DeleteProductRequest(slug);
            await Sender.Send(request);
            return NoContent();
        }

        [HttpPut]
        public async Task<CreatProductUOMResponse> Put([FromBody] UpdateProductUomRequest putproduct)
        {
            return await Sender.Send(putproduct);
        }
}

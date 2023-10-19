using Warehouse.Contracts.ProductUOM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Warehouse.Api.Utilities;

namespace Warehouse.Api.Controllers
{
    [Authorize]
    public class ProductUOMsController : ApiControllerBase
    {
        [Authorize(Roles = $"{Constants.SuperAdminRoleName},{Constants.BuyerRoleName}")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProductUOMRequest request)
        {
            var response = await Sender.Send(request);
            return Created($"productuoms/{response.Slug}", response);
        }
        [HttpGet]
        public async Task<List<SingleProductUomResponse>> Get([FromQuery] GetProductsUomQuery query)
        {
            return await Sender.Send(query);
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

        [Authorize(Roles = $"{Constants.SuperAdminRoleName},{Constants.BuyerRoleName}")]
        [HttpDelete("{slug}")]
        public async Task<IActionResult> Delete(string slug)
        {
            var request = new DeleteProductUomRequest(slug);
            await Sender.Send(request);
            return NoContent();
        }

        [Authorize(Roles = $"{Constants.SuperAdminRoleName},{Constants.BuyerRoleName}")]
        [HttpPut("{id}")]
        public async Task<SingleProductUomResponse> Put(Guid id, [FromBody] UpdateProductUomRequest request)
        {
            request.Id = id;
            return await Sender.Send(request);
        }
    }
}

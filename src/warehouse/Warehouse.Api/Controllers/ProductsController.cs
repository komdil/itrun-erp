using Warehouse.Contracts.Product;
using Microsoft.AspNetCore.Mvc;

namespace Warehouse.Api.Controllers
{
    public class ProductsController : ApiControllerBase
    {
        private IConfiguration _configuration;

        public ProductsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

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

        [HttpGet("GetSome/{name}")]
        public IActionResult GetSome(string name)
        {
            return Ok(_configuration[name]);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProductRequest request)
        {
            var response = await Sender.Send(request);
            return Created($"products/{response.Id}", response);
        }

        [HttpPut]
        public async Task<SingleProductResponse> Put([FromBody] UpdateProductRequest request)
        {
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

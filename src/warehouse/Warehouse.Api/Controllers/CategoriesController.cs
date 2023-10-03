using Warehouse.Contracts.Warehouse;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Contracts.Categories;

namespace Warehouse.Api.Controllers
{
    public class CategoriesController : ApiControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCategoryRequest request)
        {
            var response = await Sender.Send(request);
            return Created($"categories/{response.Slug}", response);
        }

        [HttpGet]
        public async Task<List<SingleCategoryResponse>> Get([FromQuery] GetCategoryQuery query)
        {
            return await Sender.Send(query);
        }
    }
}

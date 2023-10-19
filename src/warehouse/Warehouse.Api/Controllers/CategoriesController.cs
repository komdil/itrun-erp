using Warehouse.Contracts.Warehouse;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Contracts.Categories;
using Microsoft.AspNetCore.Authorization;
using Warehouse.Api.Utilities;

namespace Warehouse.Api.Controllers
{
    [Authorize]
    public class CategoriesController : ApiControllerBase
    {
        [Authorize(Roles = $"{Constants.SuperAdminRoleName},{Constants.BuyerRoleName}")]
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

        [HttpGet("{id}")]
        public async Task<SingleCategoryResponse> Get(Guid id)
        {
            var query = new GetSingleCategoryQuery()
            {
                Id = id
            };
            return await Sender.Send(query);
        }

        [Authorize(Roles = $"{Constants.SuperAdminRoleName},{Constants.BuyerRoleName}")]
        [HttpPut("{id}")]
        public async Task<SingleCategoryResponse> Put(Guid id, [FromBody] UpdateCategoryRequest updateCategoryRequest)
        {
            updateCategoryRequest.Id = id;
            return await Sender.Send(updateCategoryRequest);
        }

        [Authorize(Roles = $"{Constants.SuperAdminRoleName},{Constants.BuyerRoleName}")]
        [HttpDelete("{name}")]
        public async Task<IActionResult> Delete(string name)
        {
            var deleteRequest = new DeleteCategoryRequest()
            {
                Name = name
            };
            await Sender.Send(deleteRequest);
            return NoContent();
        }
    }
}

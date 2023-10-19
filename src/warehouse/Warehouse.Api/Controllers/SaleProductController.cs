using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Contracts.SellProduct;
using Warehouse.Contracts.Warehouse;

namespace Warehouse.Api.Controllers
{
    [Authorize]
    public class SaleProductController : ApiControllerBase
	{
		[HttpGet]
		public async Task<List<SingleProductSellResponse>> Get([FromQuery] GetSaleProductsQuery query)
		{
			return await Sender.Send(query);
		}

		[HttpGet("{id}")]
		public async Task<SingleProductSellResponse> Get(Guid id)
		{
			var query = new GetSingleSaleProductsQuery()
			{
				Id = id
			};
			return await Sender.Send(query);
		}
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var deleteRequest = new DeleteSaleProductRequest()
			{
				Id = id
			};
			await Sender.Send(deleteRequest);
			return NoContent();
		}
		[HttpPost]
		public async Task<IActionResult> Post([FromBody] CreateSellProductRequest request)
		{
			var response = await Sender.Send(request);
			return Ok(response);
		}
	}
}

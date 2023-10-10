using Microsoft.AspNetCore.Mvc;
using Warehouse.Contracts.SellProduct;

namespace Warehouse.Api.Controllers
{
    [ApiController]
	[Route("api/sale")]
	public class SaleProductController : ApiControllerBase
	{
        [HttpPost]
		public async Task<IActionResult> SellProductAsync([FromBody] CreateSellProductRequest request)
		{
			var response = await Sender.Send(request);
			return Ok(response);
		}
	}
}

using Contracts.Product;
using Microsoft.AspNetCore.Mvc;

namespace Warehouse.Api.Controllers
{
	[ApiController]
	[Route("api/sale")]
	public class SaleProductController : ApiControllerBase
	{
        [HttpPut]
		public async Task<IActionResult> SellProductAsync([FromBody] SellProductRequest request)
		{
			
			var response = await Sender.Send(request);
			return Ok(response);
		}
	}
}

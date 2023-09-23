using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Warehouse.Api.Controllers
{
	[ApiController]
	[Route("api/sale")]
	public class SaleController : ControllerBase
	{
		private readonly List<SaleProduct> _sales = new List<SaleProduct>();
		[HttpPost]
		public IActionResult CreateSale([FromBody] SaleProduct sale)
		{

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			_sales.Add(sale);
			return Ok();
		}
	}
}

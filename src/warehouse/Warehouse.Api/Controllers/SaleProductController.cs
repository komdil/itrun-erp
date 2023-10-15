﻿using Microsoft.AspNetCore.Mvc;
using Warehouse.Contracts.SellProduct;

namespace Warehouse.Api.Controllers
{
	public class SaleProductController : ApiControllerBase
	{
        [HttpPost]
		public async Task<IActionResult> Post([FromBody] CreateSellProductRequest request)
		{
			var response = await Sender.Send(request);
			return Ok(response);
		}
	}
}
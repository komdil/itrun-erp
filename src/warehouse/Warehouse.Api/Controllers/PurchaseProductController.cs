using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Contracts.ProductPurchase;

namespace Warehouse.Api.Controllers
{
    public class PurchaseProductController : ApiControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProductPurchaseRequest request)
        {
            var response = await Sender.Send(request);
            return Ok(response);
        }
    }
}

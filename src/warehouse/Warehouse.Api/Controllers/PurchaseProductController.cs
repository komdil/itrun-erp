using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Contracts.ProductPurchase;
using Warehouse.Contracts.SellProduct;
using Warehouse.Contracts.Warehouse;

namespace Warehouse.Api.Controllers
{
    [Authorize]
    public class PurchaseProductController : ApiControllerBase
    {
        [HttpPost]
        public async Task<SingleProductPurchaseResponse> Post([FromBody] CreateProductPurchaseRequest request)
        {
            return await Sender.Send(request);
        }

        [HttpGet]
        public async Task<List<SingleProductPurchaseResponse>> Get([FromQuery] GetProductPurchasesQuery query)
        {
            return await Sender.Send(query);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleteRequest = new DeleteProductPurchaseRequest()
            {
                Id = id
            };
            await Sender.Send(deleteRequest);
            return NoContent();
        }
    }
}

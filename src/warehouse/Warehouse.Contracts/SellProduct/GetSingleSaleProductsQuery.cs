using MediatR;
namespace Warehouse.Contracts.SellProduct
{
	public record GetSingleSaleProductsQuery : IRequest<SingleProductSellResponse>
	{
		public Guid Id { get; set; }
	}
}

using MediatR;

namespace Warehouse.Contracts.SellProduct
{
	public record DeleteSaleProductRequest : IRequest
	{
		public Guid Id { get; set; }
	}
}

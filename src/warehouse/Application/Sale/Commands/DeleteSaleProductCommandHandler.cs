using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Warehouse.Contracts.SellProduct;

namespace Application.Sale.Commands
{
	public class DeleteSaleProductCommandHandler : IRequestHandler<DeleteSaleProductRequest>
	{
		IApplicationDbContext _context;
		public DeleteSaleProductCommandHandler(IApplicationDbContext context)
		{
			_context = context;
		}
		public async Task Handle(DeleteSaleProductRequest request, CancellationToken cancellationToken)
		{
			SaleProduct saleProduct = await _context.SaleProducts.FirstOrDefaultAsync(s => s.Id == request.Id);
			if (saleProduct == null)
				throw new NotFoundException();

			_context.SaleProducts.Remove(saleProduct);
			await _context.SaveChangesAsync();
		}
	}
}

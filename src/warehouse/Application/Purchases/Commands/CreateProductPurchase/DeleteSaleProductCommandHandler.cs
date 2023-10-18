using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Warehouse.Contracts.SellProduct;

namespace Application.Sale.Commands
{
    public class DeleteProductPurchaseCommandHandler : IRequestHandler<DeleteProductPurchaseRequest>
    {
        IApplicationDbContext _context;
        public DeleteProductPurchaseCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Handle(DeleteProductPurchaseRequest request, CancellationToken cancellationToken)
        {
            ProductPurchase productPurchase = await _context.ProductPurchases.FirstOrDefaultAsync(s => s.Id == request.Id);
            if (productPurchase == null)
                throw new NotFoundException();

            _context.ProductPurchases.Remove(productPurchase);
            await _context.SaveChangesAsync();
        }
    }
}

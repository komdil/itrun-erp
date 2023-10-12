using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Warehouse.Contracts.Product;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductRequest>
    {
        IApplicationDbContext _dbContext;

        public DeleteProductCommandHandler(IApplicationDbContext dbcontext) => _dbContext = dbcontext;

        public async Task Handle(DeleteProductRequest request, CancellationToken cancellationToken)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(prod => prod.Name == request.Slug, cancellationToken);

            if (product == null)
                throw new NotFoundException();

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}

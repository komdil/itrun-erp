using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Products.Commands.UpdateProduct;
using AutoMapper;
using Contracts.Product;
using Domain.Entities;
using MediatR;

namespace Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductRequest>
    {
        IApplicationDbContext _dbContext;

        public DeleteProductCommandHandler(IApplicationDbContext dbcontext) => _dbContext = dbcontext;

        public async Task Handle(DeleteProductRequest request, CancellationToken cancellationToken)
        {
            var product = _dbContext.Products.FirstOrDefault(prod => prod.Name == request.Slug);
                //.FindAsync(new object[] { productNameToDelete }, cancellationToken);

            if (product == null)
            {
                throw new NotFoundException(request.Slug);
            }

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}

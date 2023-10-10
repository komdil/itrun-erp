using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Warehouse.Contracts.Product;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using Warehouse.Contracts.ProductUOM;

namespace Application.ProductUom.Commands.DeleteProductUom
{
    public class DeleteProductUomCommandHandler : IRequestHandler<DeleteProductUomRequest>
    {
        IApplicationDbContext _dbContext;

        public DeleteProductUomCommandHandler(IApplicationDbContext dbcontext) => _dbContext = dbcontext;

        public async Task Handle(DeleteProductUomRequest request, CancellationToken cancellationToken)
        {
            var productuom = await _dbContext.ProductUOMs.FirstOrDefaultAsync(prod => prod.Name == request.Slug, cancellationToken);

            if (productuom == null)
                throw new NotFoundException();

            _dbContext.ProductUOMs.Remove(productuom);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}

using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Contracts.Product;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductRequest>
    {
        IApplicationDbContext _dbContext;
        IMapper _mapper;

        public UpdateProductCommandHandler(IApplicationDbContext dbcontext, IMapper mapper)
        {
            _dbContext = dbcontext;
            _mapper = mapper;
        }

        public async Task Handle(UpdateProductRequest request, CancellationToken cancellationToken)
        {
            var productUom = await _dbContext.ProductUOMs.FirstOrDefaultAsync(pUom => pUom.Name == request.ProductUom);
            if (productUom == null)
            {
                throw new NotFoundException(request.ProductUom);
            }

            var product = await _dbContext.Products.FirstOrDefaultAsync(prod => prod.Id == request.Id);

            if (product == null)
            {
                throw new NotFoundException(request.Name);
            }

            product = _mapper.Map<Product>(request);
            product.Uom = productUom;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}

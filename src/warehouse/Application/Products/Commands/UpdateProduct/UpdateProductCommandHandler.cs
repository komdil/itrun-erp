using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Warehouse.Contracts.Product;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductRequest, SingleProductResponse>
    {
        IApplicationDbContext _dbContext;
        IMapper _mapper;

        public UpdateProductCommandHandler(IApplicationDbContext dbcontext, IMapper mapper)
        {
            _dbContext = dbcontext;
            _mapper = mapper;
        }

        public async Task<SingleProductResponse> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(prod => prod.Id == request.Id, cancellationToken: cancellationToken);

            if (product == null)
                throw new NotFoundException();

            var productUom = await _dbContext.ProductUOMs.FirstOrDefaultAsync(pUom => pUom.Name == request.ProductUom, cancellationToken: cancellationToken);
            if (productUom == null)
                throw new ValidationFailedException(request.ProductUom);

            product = _mapper.Map<Product>(request);
            product.Uom = productUom;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return _mapper.Map<SingleProductResponse>(product);
        }
    }
}

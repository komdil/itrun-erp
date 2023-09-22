using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Warehouse.Contracts.Product;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductRequest, SingleProductResponse>
    {
        IApplicationDbContext _dbContext;
        IMapper _mapper;

        public CreateProductCommandHandler(IApplicationDbContext dbcontext, IMapper mapper)
        {
            _dbContext = dbcontext;
            _mapper = mapper;
        }

        public async Task<SingleProductResponse> Handle(CreateProductRequest request, CancellationToken cancellationToken)
        {
            var productUom = await _dbContext.ProductUOMs.FirstOrDefaultAsync(pUom => pUom.Name == request.ProductUom, cancellationToken: cancellationToken);
            if (productUom == null)
                throw new ValidationFailedException(request.ProductUom);

            var product = _mapper.Map<Product>(request);
            product.Uom = productUom;

            await _dbContext.Products.AddAsync(product, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<SingleProductResponse>(product);
        }
    }
}

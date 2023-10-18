using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Warehouse.Contracts.Exceptions;
using Warehouse.Contracts.ProductPurchase;

namespace Application.ProductPurchases.Commands.CreateProductPurchase
{
    public class CreateProductPurchaseCommandHandler : IRequestHandler<CreateProductPurchaseRequest, SingleProductPurchaseResponse>
    {
        private readonly IApplicationDbContext _dbcontext;
        private readonly IMapper _mapper;

        public CreateProductPurchaseCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbcontext = dbContext;
            _mapper = mapper;
        }
        public async Task<SingleProductPurchaseResponse> Handle(CreateProductPurchaseRequest request, CancellationToken cancellationToken)
        {
            var uom = await _dbcontext.ProductUOMs.FirstOrDefaultAsync(u => u.Abbreviation == request.ProductUom, cancellationToken);

            if (uom == null)
                throw new ValidationFailedException("Product UOM", request.ProductUom);

            var product = await _dbcontext.Products.FirstOrDefaultAsync(p => p.Name == request.ProductName, cancellationToken);

            if (product == null)
                throw new ValidationFailedException("Product", request.ProductName);
            product.Quantity += request.Quantity;
            product.Price = request.Price;
            var prod = _mapper.Map<ProductPurchase>(request);

            await _dbcontext.ProductPurchases.AddAsync(prod);
            await _dbcontext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<SingleProductPurchaseResponse>(prod);
        }
    }
}

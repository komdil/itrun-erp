using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Warehouse.Contracts.SellProduct;

namespace Application.Sale.Commands
{
    public class CreateProductSaleCommandHandler : IRequestHandler<CreateSellProductRequest, SingleProductSellResponse>
    {
        private readonly IApplicationDbContext _dbcontext;
        private readonly IMapper _mapper;

        public CreateProductSaleCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbcontext = dbContext;
            _mapper = mapper;
        }

        public async Task<SingleProductSellResponse> Handle(CreateSellProductRequest request, CancellationToken cancellationToken)
        {
            var product = await _dbcontext.Products.FirstOrDefaultAsync(p => p.Name == request.ProductName, cancellationToken);
            if (product == null)
                throw new ValidationFailedException("Product not found");

            if (product.Quantity < request.Quantity)
            {
                throw new ValidationFailedException("Product is not enough to sale");
            }
            else
            {
                product.Quantity -= request.Quantity;
                product.Price = request.Price;
            }
            var uom = await _dbcontext.ProductUOMs.FirstOrDefaultAsync(u => u.Abbreviation == request.ProductUom, cancellationToken);
            if (uom == null)
                throw new ValidationFailedException("Product UOM", request.ProductUom);

            var prod = _mapper.Map<SaleProduct>(request);
            await _dbcontext.SaleProducts.AddAsync(prod);
            await _dbcontext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<SingleProductSellResponse>(prod);
        }

    }
}

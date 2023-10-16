using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Warehouse.Contracts.SellProduct;

namespace Application.Sale.Commands
{
	public class CreateProductPurchaseCommandHandler : IRequestHandler<CreateSellProductRequest, SingleProductSellResponse>
    {
        private readonly IApplicationDbContext _dbcontext;
        private readonly IMapper _mapper;

        public CreateProductPurchaseCommandHandler(IApplicationDbContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }

        public async Task<SingleProductSellResponse> Handle(CreateSellProductRequest request, CancellationToken cancellationToken)
        {
            var uom = await _dbcontext.ProductUOMs.FirstOrDefaultAsync(u => u.Name == request.ProductUom, cancellationToken);
            if (uom == null)
                throw new NotFoundException();

            var product = await _dbcontext.Products.FirstOrDefaultAsync(p => p.Name == request.ProductName, cancellationToken);

            if (product == null)
            {
                product = new Product { Name = request.ProductName, Uom = uom, Price = request.Price, Quantity = request.Quantity };
                await _dbcontext.Products.AddAsync(product);
            }
            else if (product.Quantity < request.Quantity)
            {
                throw new ValidationFailedException(request.ProductName);
            }
            else
            {
                product.Quantity -= request.Quantity;
                product.Price = request.Price;
            }

            var prod = _mapper.Map<SaleProduct>(request);
            await _dbcontext.SaleProducts.AddAsync(prod);
            await _dbcontext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<SingleProductSellResponse>(prod);
        }

    }
}

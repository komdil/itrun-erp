using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Warehouse.Contracts.ProductPurchase;

namespace Application.ProductPurchases.Commands.CreateProductPurchase
{
    public class CreateProductPurchaseCommandHandler : IRequestHandler<CreateProductPurchaseRequest, SingleProductPurchaseResponse>
    {
        private readonly IApplicationDbContext _dbcontext;
        private readonly IMapper _mapper;

        public CreateProductPurchaseCommandHandler(IApplicationDbContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }
        public async Task<SingleProductPurchaseResponse> Handle(CreateProductPurchaseRequest request, CancellationToken cancellationToken)
        {
            var product = await _dbcontext.Products.FirstOrDefaultAsync(p => p.Name == request.ProductName, cancellationToken);
            var uom = await _dbcontext.ProductUOMs.FirstOrDefaultAsync(u => u.Name == request.ProductUom, cancellationToken);

            if (uom == null)
                throw new NotFoundException();

            if (product == null)
            {
                product = new Product { Name = request.ProductName, Uom = uom, Price = request.Price, Quantity = request.Quantity };
            }
            else
            {
                product.Quantity += request.Quantity;
            }
            var prod = _mapper.Map<ProductPurchase>(request);

            await _dbcontext.ProductPurchases.AddAsync(prod);
            await _dbcontext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<SingleProductPurchaseResponse>(prod);
        }
    }
}

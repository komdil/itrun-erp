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
            if (product == null)
                throw new NotFoundException();

            if (product.Quantity < request.Quantity)
                throw new ValidationFailedException(request.ProductName);

            product.Quantity -= request.Quantity;
            var prod = _mapper.Map<ProductPurchase>(request);

            await _dbcontext.ProductPurchases.AddAsync(prod);
            await _dbcontext.SaveChangesAsync();

            return _mapper.Map<SingleProductPurchaseResponse>(prod);
        }
    }
}

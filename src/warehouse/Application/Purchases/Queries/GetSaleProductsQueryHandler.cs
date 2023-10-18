using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Warehouse.Contracts.ProductPurchase;
using Warehouse.Contracts.Warehouse;

namespace Application.Sale.Queries
{
    public class GetProductPurchasesQueryHandler : IRequestHandler<GetProductPurchasesQuery, List<SingleProductPurchaseResponse>>
    {
        IApplicationDbContext _dbContext;
        IMapper _mapper;

        public GetProductPurchasesQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<List<SingleProductPurchaseResponse>> Handle(GetProductPurchasesQuery request, CancellationToken cancellationToken)
        {
            IQueryable<ProductPurchase> productPurchases = _dbContext.ProductPurchases;
            if (!string.IsNullOrWhiteSpace(request.ProductName))
                productPurchases = productPurchases.Where(p => p.ProductName == request.ProductName);

            if (!string.IsNullOrWhiteSpace(request.ProductUom))
                productPurchases = productPurchases.Where(p => p.ProductUom == request.ProductUom);


            productPurchases = productPurchases.Take(request.PageSize);

            return await productPurchases.ProjectTo<SingleProductPurchaseResponse>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        }
    }
}

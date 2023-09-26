using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Warehouse.Contracts.Product;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Application.Products.Queries.GetProductList
{
    public class GetProductListQueryHandler : IRequestHandler<GetProductsQuery, List<SingleProductResponse>>
    {
        IApplicationDbContext _dbContext;
        IMapper _mapper;

        public GetProductListQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<SingleProductResponse>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Product> productQuery = _dbContext.Products;

            if (!string.IsNullOrWhiteSpace(request.Manufacturer))
                productQuery = productQuery.Where(p => p.Manufacturer == request.Manufacturer);

            if (!string.IsNullOrWhiteSpace(request.Category))
                productQuery = productQuery.Where(p => p.Category == request.Category);

            productQuery = productQuery.Skip(request.StartIndex).Take(request.EndIndex);

            return await productQuery.ProjectTo<SingleProductResponse>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        }
    }
}

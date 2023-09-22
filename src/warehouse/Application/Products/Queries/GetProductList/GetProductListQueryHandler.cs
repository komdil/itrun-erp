using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Warehouse.Contracts.Product;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Queries.GetProductList
{
    public class GetProductListQueryHandler : IRequestHandler<GetProductListQueryRequest, ProductListResponse>
    {
        IApplicationDbContext _dbContext;
        IMapper _mapper;

        public GetProductListQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ProductListResponse> Handle(GetProductListQueryRequest request, CancellationToken cancellationToken)
        {
            List<GetProductListQueryResponse> productQuery = null;

            if (!string.IsNullOrWhiteSpace(request.Manufacturer) && !string.IsNullOrWhiteSpace(request.Category))
            {
                productQuery = await _dbContext.Products
                    .Where(product => product.Manufacturer == request.Manufacturer && product.Category == request.Category)
                    .ProjectTo<GetProductListQueryResponse>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
            }
            else if (!string.IsNullOrWhiteSpace(request.Manufacturer) && string.IsNullOrWhiteSpace(request.Category))
            {
                productQuery = await _dbContext.Products
                    .Where(product => product.Manufacturer == request.Manufacturer)
                    .ProjectTo<GetProductListQueryResponse>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
            }
            else if (string.IsNullOrWhiteSpace(request.Manufacturer) && !string.IsNullOrWhiteSpace(request.Category))
            {
                productQuery = await _dbContext.Products
                    .Where(product => product.Category == request.Category)
                    .ProjectTo<GetProductListQueryResponse>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
            }

            if (productQuery == null)
            {
                throw new NotFoundException("BadRequest");
            }

            return new ProductListResponse { ProductsList = productQuery };
        }
    }
}

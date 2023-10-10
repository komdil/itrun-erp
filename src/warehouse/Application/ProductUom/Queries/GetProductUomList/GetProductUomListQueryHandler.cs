using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Warehouse.Contracts.Product;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Warehouse.Contracts.ProductUOM;

namespace Application.ProductUom.Queries.GetProductUomList
{
    public class GetProductUomListQueryHandler : IRequestHandler<GetProductsUomQuery, List<SingleProductUomResponse>>
    {
        IApplicationDbContext _dbContext;
        IMapper _mapper;

        public GetProductUomListQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<SingleProductUomResponse>> Handle(GetProductsUomQuery request, CancellationToken cancellationToken)
        {
            IQueryable<ProductUOM> productQuery = _dbContext.ProductUOMs;

            if (!string.IsNullOrWhiteSpace(request.Name))
                productQuery = productQuery.Where(p => p.Name == request.Name);

            if (!string.IsNullOrWhiteSpace(request.Abbreviation))
                productQuery = productQuery.Where(p => p.Abbreviation == request.Abbreviation);

            return await productQuery.ProjectTo<SingleProductUomResponse>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        }
    }
}

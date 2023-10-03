using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Warehouse.Contracts.Warehouse;
using Warehouse.Contracts.Categories;

namespace Application.Categories
{
    internal class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, List<SingleCategoryResponse>>
        {
            IApplicationDbContext _dbContext;
            IMapper _mapper;

            public GetCategoryQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }

            public async Task<List<SingleCategoryResponse>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
            {
                IQueryable<Category> warehouseQuery = _dbContext.Warehouses;

                if (!string.IsNullOrWhiteSpace(request.Name))
                    warehouseQuery = warehouseQuery.Where(p => p.Name == request.Name);

                if (!string.IsNullOrWhiteSpace(request.Location))
                    warehouseQuery = warehouseQuery.Where(p => p.Location == request.Location);

                if (!string.IsNullOrWhiteSpace(request.Details))
                    warehouseQuery = warehouseQuery.Where(p => p.Details == request.Details);

                warehouseQuery = warehouseQuery.Skip(request.StartIndex).Take(request.EndIndex);

                return await warehouseQuery.ProjectTo<SingleCategoryResponse>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
            }
        }
    }
}

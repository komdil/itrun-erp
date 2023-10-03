using Application.Common.Interfaces;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Contracts.Product;
using Warehouse.Contracts.Warehouse;
using Microsoft.EntityFrameworkCore;

namespace Application.Warehouse
{
    public class GetWarehouseQueryHandler : IRequestHandler<GetWarehouseQuery, List<SingleWarehouseResponse>>
    {
        IApplicationDbContext _dbContext;
        IMapper _mapper;

        public GetWarehouseQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<SingleWarehouseResponse>> Handle(GetWarehouseQuery request, CancellationToken cancellationToken)
        {
            IQueryable<WareHouse> warehouseQuery = _dbContext.Warehouses;

            if (!string.IsNullOrWhiteSpace(request.Name))
                warehouseQuery = warehouseQuery.Where(p => p.Name == request.Name);

            if (!string.IsNullOrWhiteSpace(request.Location))
                warehouseQuery = warehouseQuery.Where(p => p.Location == request.Location);

            if (!string.IsNullOrWhiteSpace(request.Details))
                warehouseQuery = warehouseQuery.Where(p => p.Details == request.Details);

            warehouseQuery = warehouseQuery.Skip(request.StartIndex).Take(request.EndIndex);

            return await warehouseQuery.ProjectTo<SingleWarehouseResponse>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        }
    }
}

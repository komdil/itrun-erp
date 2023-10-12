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
using Application.Common.Exceptions;

namespace Application.Warehouse
{
    public class GetSingleWarehouseQueryHandler : IRequestHandler<GetSingleWarehouseQuery, SingleWarehouseResponse>
    {
        IApplicationDbContext _dbContext;
        IMapper _mapper;

        public GetSingleWarehouseQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<SingleWarehouseResponse> Handle(GetSingleWarehouseQuery request, CancellationToken cancellationToken)
        {
            WareHouse warehouse = await _dbContext.Warehouses.FirstOrDefaultAsync(s => s.Id == request.Id);
            if (warehouse == null)
                throw new NotFoundException();
            return _mapper.Map<SingleWarehouseResponse>(warehouse);
        }
    }
}

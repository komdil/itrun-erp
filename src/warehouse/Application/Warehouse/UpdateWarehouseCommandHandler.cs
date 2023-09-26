using Application.Common.Interfaces;
using Warehouse.Contracts.Warehouse;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Application.Warehouse
{
    public class UpdateWarehouseCommandHandler : IRequestHandler<UpdateWarehouseRequest, SingleWarehouseResponse>
    {
        IApplicationDbContext _context;
        IMapper _mapper;
        public UpdateWarehouseCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _context = dbContext;
            _mapper = mapper;
        }

        public async Task<SingleWarehouseResponse> Handle(UpdateWarehouseRequest request, CancellationToken cancellationToken)
        {
            WareHouse warehouse = await _context.Warehouses.FirstOrDefaultAsync(s => s.Id == request.Id);
            if (warehouse == null)
                throw new NotFoundException();

            warehouse.Name = request.Name;
            warehouse.Details = request.Details;
            warehouse.Location = request.Location;

            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<SingleWarehouseResponse>(request);
        }
    }
}

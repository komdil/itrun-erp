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

namespace Application.Warehouse
{
    public class DeleteWarehouseCommandHandler : IRequestHandler<DeleteWarehouseRequest>
    {
        IApplicationDbContext _context;
        public DeleteWarehouseCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteWarehouseRequest request, CancellationToken cancellationToken)
        {
            WareHouse warehouse = await _context.Warehouses.FirstOrDefaultAsync(s => s.Id == request.Id);
            if (warehouse == null)
                throw new NotFoundException();

            _context.Warehouses.Remove(warehouse);
            await _context.SaveChangesAsync();
        }
    }
}

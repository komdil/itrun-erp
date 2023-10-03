using Application.Common.Interfaces;
using Warehouse.Contracts.Warehouse;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Warehouse
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateWarehouseRequest, CreateWarehouseResponse>
    {
        IApplicationDbContext _context;
        public CreateCategoryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CreateWarehouseResponse> Handle(CreateWarehouseRequest request, CancellationToken cancellationToken)
        {
            var warehouse = new WareHouse()
            {
                Name = request.Name,
                Location = request.Location,
                Details = request.Details,
                Id = Guid.NewGuid(),
            };
            _context.Warehouses.Add(warehouse);
            await _context.SaveChangesAsync();
            return new CreateWarehouseResponse
            {
                Details = warehouse.Details,
                Location = warehouse.Location,
                Name = warehouse.Name,
                //TODO: Use auto mapper
            };
        }
    }
}

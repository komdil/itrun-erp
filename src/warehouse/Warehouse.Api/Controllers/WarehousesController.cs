using Contracts.Warehouse;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace Warehouse.Api.Controllers

{
    [ApiController]
    [Route("[controller]")]
    public class WarehousesController: ControllerBase
    {
        ApplicationDbContext _context;
        public WarehousesController(ApplicationDbContext context)
        {
            _context= context;
        }
        [HttpPost]
        public IActionResult Post([FromBody] CreateWarehouseRequest request)
        {
            var Warehouse = new WareHouse()
            {
                Name = request.Name,
                Location = request.Location,
                Details = request.Details,
                Id = Guid.NewGuid(),
            };
            _context.WareHouses.Add(Warehouse);
            _context.SaveChanges();
            return Created($"warehouses/{Warehouse.Id}", Warehouse);
        }
    }
}

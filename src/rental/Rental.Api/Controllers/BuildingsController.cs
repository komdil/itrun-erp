using Contracts.Buildings;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Rental.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BuildingsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public BuildingsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<ActionResult<BuildingResponse>> Post([FromBody] BuildingRequest request)
        {
            var buildingEntity = new Building
            {
                Name = request.Name,
                Address = request.Address,
                Country = request.Country,
                City = request.City,
                Area = request.Area
            };

            _dbContext.Buildings.Add(buildingEntity);
            await _dbContext.SaveChangesAsync();

            var response = new BuildingResponse
            {
                BuildingId = buildingEntity.BuildingId,
                Name = buildingEntity.Name,
                Address = buildingEntity.Address,
                Country = buildingEntity.Country,
                City = buildingEntity.City,
                Area = buildingEntity.Area
            };
            return Created($"buildings/{buildingEntity.Id}", response);
        }

        [HttpGet]
        public async Task<ActionResult<List<Building>>> Get()
        {
            var buildings = await _dbContext.Buildings.ToListAsync();
            return Ok(buildings);
        }

        [HttpGet("{Id}")]

        public async Task<ActionResult<Building>> Get(string Id)
        {
            var buildings = await _dbContext.Buildings.FirstOrDefaultAsync(s=>s.BuildingId==Id);
            if (buildings == null)
                return NotFound();
            return Ok(buildings);
        }
    }
}

    
        
       
            

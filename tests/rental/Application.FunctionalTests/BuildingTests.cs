using Contracts.Buildings;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTests
{
    public class BuildingTests
    {
        private HttpClient _httpClient;
        private IServiceScopeFactory _scopeFactory;
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var factory = new CustomWebApplicationFactory();
            _scopeFactory = factory.Services.GetRequiredService<IServiceScopeFactory>();
            _httpClient = factory.CreateClient();
        }

        [Test]
        public async Task CreateBuilding_ShouldSucceed()
        {
            // Arrange
            var buildingToCreate = new BuildingRequest
            {
                Name = "Sample Building",
                Address = "123 Main Street",
                Country = "United States",
                City = "Example City",
                Area = 1500,
                BuildingId="ABC"
            };

            var content = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(buildingToCreate),
                Encoding.UTF8,
                "application/json");

            // Act
            var response = await _httpClient.PostAsync("/buildings", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var createdBuilding = await response.Content.ReadFromJsonAsync<BuildingResponse>();

            createdBuilding.Should().NotBeNull();
            createdBuilding.Name.Should().Be("Sample Building");
            createdBuilding.Address.Should().Be("123 Main Street");
            createdBuilding.Country.Should().Be("United States");
            createdBuilding.City.Should().Be("Example City");
            createdBuilding.Area.Should().Be(1500);
        }

        [Test]
        public async Task CreateBuilding_ShouldBeSavedToDB()
        {
            // Arrange
            var buildingToCreate = new BuildingRequest
            {
                Name = "Sample Building",
                Address = "123 Main Street",
                Country = "United States",
                City = "Example City",
                Area = 1500,
                BuildingId="ABC"
            };

            var content = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(buildingToCreate),
                Encoding.UTF8,
                "application/json");

            // Act
            var response = await _httpClient.PostAsync("/buildings", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var buildings = await GetBuildingsAsync();
            var createdBuildingInDb = buildings.FirstOrDefault(b => b.Name == "Sample Building");

            createdBuildingInDb.Should().NotBeNull();
            createdBuildingInDb.Address.Should().Be("123 Main Street");
            createdBuildingInDb.Country.Should().Be("United States");
            createdBuildingInDb.City.Should().Be("Example City");
            createdBuildingInDb.Area.Should().Be(1500);
        }

        [Test]
        public async Task GetBuildingsTest()
        {
            // Arrange
            await CreateBuildingsAsync("123");

            // Act
            var response = await _httpClient.GetAsync("/buildings");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var buildings = await response.Content.ReadFromJsonAsync<List<BuildingResponse>>();
            buildings.Count.Should().Be(1);
            buildings[0].BuildingId.Should().Be("123");             
        }

        [Test]
        public async Task GetBuildingsByIdTest()
        {
            // Arrange
            await CreateBuildingsAsync("123");

            // Act
            var response = await _httpClient.GetAsync("/buildings/123");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var building = await response.Content.ReadFromJsonAsync<BuildingResponse>();
            building.BuildingId.Should().Be("123");
        }

        [Test]
        public async Task GetBuildingsByIdNotFoundTest()
        {
            // Arrange
            await CreateBuildingsAsync("123");

            // Act
            var response = await _httpClient.GetAsync("//buildings//1234");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);           
        }

        async Task<List<Building>> GetBuildingsAsync()
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            return await dbContext.Buildings.ToListAsync();
        }

        async Task<Building> CreateBuildingsAsync(string buildingId)
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var building = new Building
            {
                BuildingId = buildingId,
                Id = Guid.NewGuid(),

            };
            dbContext.Buildings.Add(building);
            await dbContext.SaveChangesAsync();
            return building;

            }
    }
}

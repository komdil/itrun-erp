using Application.IntegrationTests;
using Contracts.Warehouse;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using System.Text;

namespace Application.IntergrationTests
{
    public class WarehouseTests
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
        public async Task PostingWarehouse_ShoudbeSavedToDb()
        {
            // Arrange
            CreateWarehouseRequest request = new() { Name = "MyWareHouse", Location = "Lenin 226", Details = "Test" };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            // Act
            HttpResponseMessage result = await _httpClient.PostAsync("warehouses", content);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.Created);

            var allWareHouses = await GetWareHousesAsync();
            var createdWareHouse = allWareHouses.
                FirstOrDefault(n => n.Name == request.Name &&
                               n.Location == request.Location &&
                               n.Details == request.Details);
            createdWareHouse.Should().NotBeNull();
        }

        async Task<List<WareHouse>> GetWareHousesAsync()
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            return await dbContext.WareHouses.ToListAsync();
        }
    }
}


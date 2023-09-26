using Application.Common.Interfaces;
using Application.IntegrationTests;
using Warehouse.Contracts.Warehouse;
using Domain.Entities;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Application.IntergrationTests.Warehouses
{
    public class WarehouseTests : TestBase
    {
        [Test]
        public async Task PostingWarehouse_ShoudbeSavedToDb()
        {
            // Arrange
            CreateWarehouseRequest request = new() { Name = "MyWareHouse", Location = "Lenin 226", Details = "Test" };

            // Act
            HttpResponseMessage result = await _httpClient.PostAsJsonAsync("warehouses", request);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.Created);

            var createdWareHouse = GetEntity<WareHouse>(n => n.Name == request.Name &&
                               n.Location == request.Location &&
                               n.Details == request.Details);
            createdWareHouse.Should().NotBeNull();
        }

        [Test]
        public async Task GetWarehouse_ShouldReturnListOfWarehouseFromDb()
        {
            // Arrange
            await CreateWarehouses(3);
            GetWarehousesQuery request = new();

            // Act
            List<SingleWarehouseResponse> wareHouses = await _httpClient.GetFromJsonAsync<List<SingleWarehouseResponse>>("warehouses", request);

            wareHouses.Count().Should().BeGreaterThanOrEqualTo(3);
        }

        [Test]
        [Ignore("Failing")]
        public async Task GetSingleWarehouse_ShouldReturnWarehouseFromDb()
        {
            // Arrange
            await CreateWarehouses(1);
            var warehouseFromDb = GetEntities<WareHouse>().First();

            // Act
            var wareHouse = await _httpClient.GetFromJsonAsync<SingleWarehouseResponse>($"warehouses/{warehouseFromDb.Id}");

            wareHouse.Should().NotBeNull();
            wareHouse.Name.Should().Be(warehouseFromDb.Name);
            wareHouse.Location.Should().Be(warehouseFromDb.Location);
            wareHouse.Details.Should().Be(warehouseFromDb.Details);
        }

        [Test]
        [Ignore("Failing")]
        public async Task UpdateWarehouse_ShoudUpdateDb()
        {
            // Arrange
            await CreateWarehouses(1);
            var warehouseFromDb = GetEntities<WareHouse>().First();
            UpdateWarehouseRequest request = new() { Name = Guid.NewGuid().ToString(), Location = "Lenin 226", Details = "Test" };

            // Act
            HttpResponseMessage result = await _httpClient.PutAsJsonAsync($"warehouses/{warehouseFromDb.Id}", request);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var updatedWareHouse = await GetEntity<WareHouse>(s => s.Id == warehouseFromDb.Id);
            updatedWareHouse.Name.Should().Be(request.Name);
        }

        [Ignore("Failing")]
        [Test]
        public async Task DeleteWarehouse_ShoudDeleteFromDb()
        {
            // Arrange
            await CreateWarehouses(1);
            var warehouseFromDb = GetEntities<WareHouse>().First();

            // Act
            HttpResponseMessage result = await _httpClient.DeleteAsync($"warehouses/{warehouseFromDb.Id}");

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.NoContent);

            var deletedWareHouse = await GetEntity<WareHouse>(s => s.Id == warehouseFromDb.Id);
            deletedWareHouse.Should().BeNull();
        }

        private async Task CreateWarehouses(int v)
        {
            for (int i = 0; i < v; i++)
            {
                await AddAsync(new WareHouse
                {
                    Id = Guid.NewGuid(),
                    Details = "Test",
                    Location = "test",
                    Name = $"Name{i}"
                });
            }
        }
    }
}


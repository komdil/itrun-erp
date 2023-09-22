using Application.Common.Interfaces;
using Application.IntegrationTests;
using Warehouse.Contracts.Warehouse;
using Domain.Entities;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using System.Text;

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
    }
}


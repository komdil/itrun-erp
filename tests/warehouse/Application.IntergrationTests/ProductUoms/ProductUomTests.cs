using Contracts.ProductUOM;
using Contracts.Warehouse;
using Domain.Entities;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using System.Text;

namespace Application.IntergrationTests.ProductUoms
{
    public class ProductUomTests : TestBase
    {
        [Test]
        public async Task CreatProductUOM_ShouldReturnSuccess()
        {
            // Arrange
            CreateProductUOMRequest request = new() { Name = "Kilo", Abbreviation = "KG", Details = "Test" };

            // Act
            HttpResponseMessage result = await _httpClient.PostAsJsonAsync("productuoms", request);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.Created);

            var createdProductUom = await GetEntity<ProductUOM>(
                               n => n.Name == request.Name &&
                               n.Abbreviation == request.Abbreviation &&
                               n.Details == request.Details);
            createdProductUom.Should().NotBeNull();
        }
    }
}

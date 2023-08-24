using Contracts.Exceptions;
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

        [Test]
        public async Task CreateProductUOM_ShouldReturnBadRequest_WhenNameIsEmpty()
        {
            // Arrange
            CreateProductUOMRequest request = new() { Name = string.Empty, Abbreviation = "KG", Details = "Test" };

            // Act
            HttpResponseMessage result = await _httpClient.PostAsJsonAsync("productuoms", request);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            ErrorResponse errorResponse = await result.Content.ReadFromJsonAsync<ErrorResponse>();
            errorResponse.Errors.Count.Should().Be(1);

            var error = errorResponse.Errors[0];
            error.PropertyName.Should().Be("Name");
            error.Message.Should().Be("'Name' must not be empty.");
        }

    }
}

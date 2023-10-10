using Warehouse.Contracts.Exceptions;
using Warehouse.Contracts.ProductUOM;
using Warehouse.Contracts.Warehouse;
using Domain.Entities;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using Warehouse.Contracts.Product;

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

        [Test]
        public async Task DeleteProduct_ShouldReturnException_WhenSlugIsInvalid()
        {
            // Arrange
            DeleteProductRequest DeleteProductUomRequest = new("-");

            // Act
            HttpResponseMessage result = await _httpClient.DeleteAsync($"productuom/{DeleteProductUomRequest.Slug}");

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Test]
        public async Task UpdateProduct_ShouldReturnSuccess()
        {
            var validProduct = await CreateProductUom();

            UpdateProductUomRequest updateProductUomRequest = new()
            {
                Id = validProduct.Id,
                Name = "Apple",
                Abbreviation = "G",
                Details = "Test"
            };

            // Act
            HttpResponseMessage updateProductUomRequestResult = await _httpClient.PutAsJsonAsync("productuoms", updateProductUomRequest);

            // Assert
            updateProductUomRequestResult.StatusCode.Should().Be(HttpStatusCode.OK);

            var updatedProductUom = await GetEntity<ProductUOM>(prod =>
                               prod.Id == updateProductUomRequest.Id &&
                               prod.Name == updateProductUomRequest.Name &&
                               prod.Abbreviation == updateProductUomRequest.Abbreviation &&
                               prod.Details == updateProductUomRequest.Details);
            updatedProductUom.Should().NotBeNull();
        }

        [Test]
        public async Task DeleteProduct_ShouldReturnNoContent_WhenSlugIsValid()
        {
            var productUomInDb = await CreateProductUom();
            DeleteProductUomRequest deleteProductUomRequest = new(productUomInDb.Name);

            // Act
            HttpResponseMessage deleteProductUomRequestResult = await _httpClient.DeleteAsync($"productuoms/{deleteProductUomRequest.Slug}");

            // Assert
            deleteProductUomRequestResult.StatusCode.Should().Be(HttpStatusCode.NoContent);

            var deletedProductUom = await GetEntity<ProductUOM>(prod =>
                                                prod.Name == productUomInDb.Name);
            deletedProductUom.Should().BeNull();
        }

        private async Task<ProductUOM> CreateProductUom()
        {
            var productUomInDb = new ProductUOM
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                Abbreviation = "kg",
                Details = "Test"
            };
            await AddAsync(productUomInDb);
            return productUomInDb;
        }
    }
}

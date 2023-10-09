using Warehouse.Contracts.Exceptions;
using Warehouse.Contracts.ProductUOM;
using Warehouse.Contracts.Warehouse;
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

            [Test]
            public async Task DeleteProduct_ShouldReturnException_WhenUrlIsInvalid()
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
                // Arrange
                CreateProductUOMRequest productUomRequest = new() { Name = "Kilo", Abbreviation = "KG", Details = "Test" };
             

                HttpResponseMessage prodUomRequestResult = await _httpClient.PostAsJsonAsync("productuoms", productUomRequest);

                var validProduct = await GetEntity<ProductUOM>(prod => prod.Name == productUomRequest.Name);

                UpdateProductUomRequest updateProductUomRequest = new()
                {
                    Id = validProduct.Id,
                    Name = "Apple",
                    Abbreviation = "G",
                    Details = "Test"
                };

                // Act
                HttpResponseMessage updateProductUomRequestResult = await _httpClient.PutAsJsonAsync("products", updateProductUomRequest);

                // Assert
                updateProductUomRequestResult.StatusCode.Should().Be(HttpStatusCode.OK);

                var updatedProductUom = await GetEntity<ProductUOM>(prod =>
                                   prod.Id == updateProductUomRequest.Id &&
                                   prod.Name == updateProductUomRequest.Name &&
                                   prod.Abbreviation == updateProductUomRequest.Abbreviation&&
                                   prod.Details == updateProductUomRequest.Details);
                updatedProductUom.Should().NotBeNull();
            }

        [Test]
        public async Task DeleteProduct_ShouldReturnNoContent_WhenSlugIsValid()
        {
            // Arrange
            CreateProductUOMRequest productUomRequest = new() { Name = "Kilo", Abbreviation = "KG", Details = "Test" };

            HttpResponseMessage prodUomRequestResult = await _httpClient.PostAsJsonAsync("productuoms", productUomRequest);

            var validProductuom = await GetEntity<ProductUom>(prod => prod.Name == productUomRequest.Name);

            DeleteProductUomRequest deleteProductUomRequest = new(validProductuom.Name);

            // Act
            HttpResponseMessage deleteProductUomRequestResult = await _httpClient.DeleteAsync($"productuom/{deleteProductUomRequest.Slug}");

            // Assert
            deleteProductUomRequestResult.StatusCode.Should().Be(HttpStatusCode.NoContent);

            var deletedProductUom = await GetEntity<ProductUom>(prod =>
                                                prod.Name == productRequest.Name);
            deletedProductUom.Should().BeNull();
        }


    }
}

using Contracts.Exceptions;
using Contracts.Product;
using Contracts.ProductUOM;
using Domain.Entities;
using FluentAssertions;
using FluentAssertions.Primitives;
using System.Net;
using System.Net.Http.Json;
using System.Xml.Linq;

namespace Application.IntergrationTests.Products
{
    public class ProductTests : TestBase
    {
        [Test]
        public async Task CreateProduct_ShouldReturnSuccess()
        {
            // Arrange
            CreateProductUOMRequest productUomRequest = new() { Name = "Kilo", Abbreviation = "KG", Details = "Test" };
            CreateProductRequest productRequest = new()
            {
                Name = "Apple",
                Category = "fruit",
                Description = "fruit from Central Asia",
                Manufacturer = "TajFruitCompany",
                Price = 100,
                Quantity = 16,
                ProductUom = productUomRequest.Name
            };
            HttpResponseMessage prodUomRequestResult = await _httpClient.PostAsJsonAsync("productuoms", productUomRequest);

            // Act
            HttpResponseMessage prodRequestResult = await _httpClient.PostAsJsonAsync("products", productRequest);

            // Assert
            prodRequestResult.StatusCode.Should().Be(HttpStatusCode.Created);

            var createdProduct = await GetEntity<Product>(
                               prod => prod.Name == productRequest.Name &&
                               prod.Category == productRequest.Category &&
                               prod.Description == productRequest.Description &&
                               prod.Manufacturer == productRequest.Manufacturer &&
                               prod.Price == productRequest.Price &&
                               prod.Quantity == productRequest.Quantity &&
                               prod.Uom.Name == productRequest.ProductUom);
            createdProduct.Should().NotBeNull();
        }

        [Test]
        public async Task UpdateProduct_ShouldReturnSuccess()
        {
            // Arrange
            CreateProductUOMRequest productUomRequest = new() { Name = "Kilo", Abbreviation = "KG", Details = "Test" };
            CreateProductRequest productRequest = new()
            {
                Name = "Apple",
                Category = "fruit",
                Description = "fruit from Central Asia",
                Manufacturer = "TajFruitCompany",
                Price = 100,
                Quantity = 16,
                ProductUom = productUomRequest.Name
            };

            HttpResponseMessage prodUomRequestResult = await _httpClient.PostAsJsonAsync("productuoms", productUomRequest);
            HttpResponseMessage prodRequestResult = await _httpClient.PostAsJsonAsync("products", productRequest);

            var validProduct = await GetEntity<Product>(prod => prod.Name == productRequest.Name);

            UpdateProductRequest updateProductRequest = new()
            {
                Id = validProduct.Id,
                Name = "Apple",
                Category = "fruit",
                Description = "fruit from Central Asia",
                Manufacturer = "TajFruitCompany",
                Price = 100,
                Quantity = 16,
                ProductUom = productUomRequest.Name
            };

            // Act
            HttpResponseMessage updateProductRequestResult = await _httpClient.PutAsJsonAsync("products", updateProductRequest);

            // Assert
            updateProductRequestResult.StatusCode.Should().Be(HttpStatusCode.NoContent);

            var updatedProduct = await GetEntity<Product>(prod =>
                               prod.Id == updateProductRequest.Id &&
                               prod.Name == updateProductRequest.Name &&
                               prod.Category == updateProductRequest.Category &&
                               prod.Description == updateProductRequest.Description &&
                               prod.Manufacturer == updateProductRequest.Manufacturer &&
                               prod.Price == updateProductRequest.Price &&
                               prod.Quantity == updateProductRequest.Quantity &&
                               prod.Uom.Name == updateProductRequest.ProductUom);
            updatedProduct.Should().NotBeNull();
        }

        [Test]
        public async Task UpdateProduct_ShouldReturnException_WhenProductNotFound()
        {
            // Arrange
            CreateProductUOMRequest productUomRequest = new() { Name = "Kilo", Abbreviation = "KG", Details = "Test" };
            UpdateProductRequest updateProductRequest = new()
            {
                Id = Guid.NewGuid(),
                Name = "Apple",
                Category = "fruit",
                Description = "fruit from Central Asia",
                Manufacturer = "TajFruitCompany",
                Price = 100,
                Quantity = 16,
                ProductUom = productUomRequest.Name
            };

            // Act
            HttpResponseMessage updateProductRequestResult = await _httpClient.PutAsJsonAsync("products", updateProductRequest);

            // Assert
            updateProductRequestResult.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var messageContent = await updateProductRequestResult.Content.ReadAsStringAsync();
            messageContent.Should().Be($"Entity \"{updateProductRequest.Name}\" not found.");
            //ex.Message.Should().Be($"Entity \"{updateProductRequest.Name}\" not found.");
            //ErrorResponse errorResponse = await updateProductRequestResult.Content.ReadFromJsonAsync<ErrorResponse>();
            //errorResponse.Errors.Count.Should().Be(1);

            //var error = errorResponse.Errors[0];
            //error.PropertyName.Should().Be(updateProductRequest.Name);
            //error.Message.Should().Be($"Entity \"{updateProductRequest.Name}\" not found.");
        }

        [Test]
        public async Task DeleteProduct_ShouldReturnNoContent_WhenSlugIsValid()
        {
            // Arrange
            CreateProductUOMRequest productUomRequest = new() { Name = "Kilo", Abbreviation = "KG", Details = "Test" };
            CreateProductRequest productRequest = new()
            {
                Name = "Apple",
                Category = "fruit",
                Description = "fruit from Central Asia",
                Manufacturer = "TajFruitCompany",
                Price = 100,
                Quantity = 16,
                ProductUom = productUomRequest.Name
            };

            HttpResponseMessage prodUomRequestResult = await _httpClient.PostAsJsonAsync("productuoms", productUomRequest);
            HttpResponseMessage prodRequestResult = await _httpClient.PostAsJsonAsync("products", productRequest);

            var validProduct = await GetEntity<Product>(prod => prod.Name == productRequest.Name);

            DeleteProductRequest deleteProductRequest = new(validProduct.Name);

            // Act
            HttpResponseMessage deleteProductRequestResult = await _httpClient.DeleteAsync($"products/{deleteProductRequest.Slug}");

            // Assert
            deleteProductRequestResult.StatusCode.Should().Be(HttpStatusCode.NoContent);

            var deletedProduct = await GetEntity<Product>(prod =>
                                                prod.Name == productRequest.Name);
            deletedProduct.Should().BeNull();
        }

        [Test]
        public async Task DeleteProduct_ShouldReturnException_WhenUrlIsInvalid()
        {
            // Arrange
            DeleteProductRequest deleteProductRequest = new("BlaBlaBla - BlaBlaBla");

            // Act
            HttpResponseMessage deleteProductRequestResult = await _httpClient.DeleteAsync($"products/{deleteProductRequest.Slug}");

            // Assert
            deleteProductRequestResult.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var messageContent = await deleteProductRequestResult.Content.ReadAsStringAsync();
            messageContent.Should().Be($"Entity \"{deleteProductRequest.Slug}\" not found.");
            //ErrorResponse errorResponse = await deleteProductRequestResult.Content.ReadFromJsonAsync<ErrorResponse>();
            //errorResponse.Errors.Count.Should().Be(1);

            //var error = errorResponse.Errors[0];
            //error.PropertyName.Should().Be(deleteProductRequest.Slug);
            //error.Message.Should().Be($"Entity \"{deleteProductRequest.Slug}\" not found.");
        }

        [Test]
        public async Task GetProducts_ShouldGiveProducts()
        {
            // Arrange
            GetProductListQueryRequest getProductsList = new() { Category = string.Empty, Manufacturer = string.Empty, ProductUomId = Guid.Empty };

            // Act
            HttpResponseMessage getProductListResult = await _httpClient
                                    .GetAsync($"products?Company={getProductsList.Category}&Manyfacturer={getProductsList.Manufacturer}");
            // Assert
            getProductListResult.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}

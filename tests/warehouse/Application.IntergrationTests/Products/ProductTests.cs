using Warehouse.Contracts.Product;
using Warehouse.Contracts.ProductUOM;
using Domain.Entities;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;

namespace Application.IntergrationTests.Products
{
    public class ProductTests : TestBase
    {
        [Test]
        public async Task CreateProduct_ShouldReturnSuccess()
        {
            await CreateCategory("fruit");
            await CreateUom("kg");
            await CreateWarehouse("fruit");

            // Arrange
            CreateProductRequest productRequest = new()
            {
                Name = "Apple",
                Category = "fruit",
                Description = "fruit from Central Asia",
                Manufacturer = "TajFruitCompany",
                Price = 100,
                Quantity = 16,
                ProductUom = "kg",
                Warehouse = "fruit"
            };
            // Act
            HttpResponseMessage prodRequestResult = await _httpClient.PostAsJsonAsync("products", productRequest);

            // Assert
            prodRequestResult.StatusCode.Should().Be(HttpStatusCode.Created);

            var createdProduct = await GetEntity<Product>(
                               prod => prod.Name == productRequest.Name &&
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
            var category = await CreateCategory("fruit");
            var uom = await CreateUom("kg");
            var warehouse = await CreateWarehouse("fruit");
            var product = await CreateProduct(category.Id, warehouse.Id, uom.Id, "My existing product", "manufacturer");
            UpdateProductRequest updateProductRequest = new()
            {
                Id = product.Id,
                Name = "Apple",
                Category = category.Name,
                Description = "fruit from Central Asia",
                Manufacturer = "TajFruitCompany",
                Price = 100,
                Quantity = 16,
                ProductUom = uom.Name,
                Warehouse = warehouse.Name,
            };

            // Act
            HttpResponseMessage updateProductRequestResult = await _httpClient.PutAsJsonAsync($"products/{product.Id}", updateProductRequest);

            // Assert
            updateProductRequestResult.StatusCode.Should().Be(HttpStatusCode.OK);

            var updatedProduct = await GetEntity<Product>(prod =>
                               prod.Id == updateProductRequest.Id &&
                               prod.Name == updateProductRequest.Name &&
                               prod.Description == updateProductRequest.Description &&
                               prod.Manufacturer == updateProductRequest.Manufacturer &&
                               prod.Price == updateProductRequest.Price &&
                               prod.Quantity == updateProductRequest.Quantity &&
                               prod.Uom.Name == updateProductRequest.ProductUom);
            updatedProduct.Should().NotBeNull();
        }

        [Test]
        public async Task DeleteProduct_ShouldReturnNoContent_WhenSlugIsValid()
        {
            var category = await CreateCategory("fruit");
            var uom = await CreateUom("kg");
            var warehouse = await CreateWarehouse("fruit");
            var product = await CreateProduct(category.Id, warehouse.Id, uom.Id, "ProductToDelete", "manufacturer");

            // Act
            HttpResponseMessage deleteProductRequestResult = await _httpClient.DeleteAsync($"products/{product.Name}");

            // Assert
            deleteProductRequestResult.StatusCode.Should().Be(HttpStatusCode.NoContent);

            var deletedProduct = await GetEntity<Product>(prod => prod.Name == product.Name);
            deletedProduct.Should().BeNull();
        }

        [Test]
        public async Task GetProducts_ShouldGiveProducts()
        {
            var category = await CreateCategory("fruit");
            var uom = await CreateUom("kg");
            var warehouse = await CreateWarehouse("fruit");
            var product = await CreateProduct(category.Id, warehouse.Id, uom.Id, "My existing product", "manufacturer");

            GetProductsQuery query = new() { Category = category.Name, Warehouse = warehouse.Name };

            // Act
            var getProductListResult = await _httpClient
                                    .GetFromJsonAsync<List<SingleProductResponse>>("products", query);
            // Assert
            getProductListResult.Count.Should().BeGreaterThanOrEqualTo(1);
        }

        private async Task<Category> CreateCategory(string name)
        {
            var res = new Category
            {
                Id = Guid.NewGuid(),
                Description = "Test",
                Name = name
            };
            await AddAsync(res);
            return res;
        }

        private async Task<ProductUOM> CreateUom(string abbrevation)
        {
            var res = new ProductUOM
            {
                Id = Guid.NewGuid(),
                Name = abbrevation,
                Abbreviation = abbrevation,
            };
            await AddAsync(res);
            return res;
        }

        private async Task<WareHouse> CreateWarehouse(string name)
        {
            var res = new WareHouse
            {
                Id = Guid.NewGuid(),
                Details = "Test",
                Name = name
            };
            await AddAsync(res);
            return res;
        }

        private async Task<Product> CreateProduct(Guid category, Guid warehouse, Guid uom, string name, string manufacturer)
        {
            var res = new Product
            {
                Id = Guid.NewGuid(),
                Name = name,
                WarehouseId = warehouse,
                CategoryId = category,
                UomId = uom,
                Manufacturer = manufacturer
            };
            await AddAsync(res);
            return res;
        }
    }
}

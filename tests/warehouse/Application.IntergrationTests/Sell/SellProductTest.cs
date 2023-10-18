using Domain.Entities;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Contracts.Product;
using Warehouse.Contracts.ProductUOM;
using Warehouse.Contracts.SellProduct;
using System.Xml.Linq;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Warehouse.Contracts.ProductPurchase;
using Warehouse.Contracts.Warehouse;
using Azure;

namespace Application.IntergrationTests.SallProduct
{
    public class SellProductTest : TestBase
    {
        [Test]
        public async Task SellProduct_ShouldNotFound()
        {
            // Arrange
            CreateSellProductRequest createSellProductRequest = new();

            //Act
            HttpResponseMessage result = await _httpClient.PostAsJsonAsync("/saleproduct", createSellProductRequest);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task CreateProductSell_SellProductQuantitymoreofProduct_BadRequest()
        {
            var warehouseId = await CreateWareHouse();
            await CreateProduct();

            CreateSellProductRequest createSellProductRequest = new()
            {
                ProductName = "TestName",
                Price = 10,
                Date = DateTime.Now,
                Comment = "Test",
                ProductUom = "kilogram",
                WareHouseId = warehouseId,
                TotalPrice = 200,
                Quantity = 20,

            };
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/saleproduct", createSellProductRequest);
            Assert.That(response.StatusCode == HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task CreateProductSell_SellExistingProduct_Success()
        {
            var category = await CreateCategory("fruit");
            var warehouseId = await CreateWareHouse();
            var prod = await CreateProduct(category.Id);
            int quantityBefore = prod.Quantity;
            CreateSellProductRequest createSellProductRequest = new()
            {
                ProductName = "TestName",
                Price = 10,
                Date = DateTime.Now,
                Comment = "Test",
                ProductUom = "kg",
                WareHouseId = warehouseId,
                TotalPrice = 100,
                Quantity = 10,
            };
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/saleproduct", createSellProductRequest);
            response.EnsureSuccessStatusCode();

            var product = await GetEntity<Product>(p => p.Name == "TestName");
            product.Quantity.Should().Be(quantityBefore - createSellProductRequest.Quantity);
        }

        [Test]
        public async Task CreateProductSell_TriggeringConcurrencyException()
        {
            // Arrange
            var warehouseId = await CreateWareHouse();
            await CreateProduct();

            var ctx1 = _scopeFactory.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var prod1 = ctx1.Products.FirstOrDefault(p => p.Name == "TestName");
            prod1.Quantity -= 2;

            var ctx2 = _scopeFactory.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var prod2 = ctx2.Products.FirstOrDefault(p => p.Name == "TestName");
            prod2.Quantity -= 5;

            ctx2.SaveChanges();
            Assert.Throws<DbUpdateConcurrencyException>(() => ctx1.SaveChanges());
        }

        [Test]
        public async Task GetSaleProduct_ShouldReturnListOfSaleProductFromDb()
        {
            // Arrange
            await CreateSaleProduct(3);
            GetSaleProductsQuery request = new();

            // Act
            List<SingleProductSellResponse> saleProduct = await _httpClient.GetFromJsonAsync<List<SingleProductSellResponse>>("saleproduct", request);

            saleProduct.Count().Should().BeGreaterThanOrEqualTo(3);
        }

        [Test]
        public async Task GetSingleSaleProduct_ShouldReturnSaleProductFromDb()
        {
            // Arrange
            await CreateSaleProduct(1);
            var SaleProductFromDb = GetEntities<SaleProduct>().First();

            // Act
            var saleProduct = await _httpClient.GetFromJsonAsync<SingleProductSellResponse>($"saleproduct/{SaleProductFromDb.Id}");

            saleProduct.Should().NotBeNull();
            saleProduct.ProductName.Should().Be(SaleProductFromDb.ProductName);
            saleProduct.ProductUom.Should().Be(SaleProductFromDb.ProductUom);
            saleProduct.Comment.Should().Be(SaleProductFromDb.Comment);
        }

        [Test]
        public async Task DeleteProductSell_ShouldDeleteFromDb()
        {
            await CreateSaleProduct(1);
            // Arrange
            var SaleProductFromDb = GetEntities<SaleProduct>().First();

            // Act
            HttpResponseMessage result = await _httpClient.DeleteAsync($"/saleproduct/{SaleProductFromDb.Id}");

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.NoContent);

            var deletedWareHouse = await GetEntity<SaleProduct>(s => s.Id == SaleProductFromDb.Id);
            deletedWareHouse.Should().BeNull();
        }

        [Test]
        public async Task DeleteProductSell_ShouldBadRequest()
        {
            // Act
            HttpResponseMessage result = await _httpClient.DeleteAsync($"/saleproduct/{10000}");

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        async Task<Guid> CreateWareHouse()
        {
            WareHouse warehouse = new()
            {
                Details = "Test",
                Id = Guid.NewGuid(),
                Location = "Tajikistan",
                Name = "Warry",
            };
            await AddAsync(warehouse);
            return warehouse.Id;
        }

        async Task<Product> CreateProduct(Guid? categoryId = null)
        {
            Product product = new()
            {
                Description = "Test",
                Id = Guid.NewGuid(),
                Manufacturer = "TTTT",
                Name = "TestName",
                Price = 12,
                Quantity = 15,
                Uom = new ProductUOM() { Name = "kilogram", Abbreviation = "kg", Details = "Test", Id = Guid.NewGuid() }
            };
            if (categoryId.HasValue)
            {
                product.CategoryId = categoryId.Value;
            }
            else
            {
                product.Category = new Category { Name = "Fruit", };
            }
            await AddAsync(product);
            return product;
        }

        async Task CreateSaleProduct(int v)
        {
            for (int i = 0; i < v; i++)
            {
                var warehouseId = await CreateWareHouse();
                SaleProduct saleProduct = new()
                {
                    ProductName = $"TestName{i}",
                    Price = 10 + i,
                    Date = DateTime.Now,
                    Comment = "Test",
                    ProductUom = "kilogram",
                    WareHouseId = warehouseId,
                    TotalPrice = 100 + i,
                    Quantity = 10 + i,

                };
                await AddAsync(saleProduct);
            }
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
    }
}

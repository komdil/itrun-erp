using Domain.Entities;
using FluentAssertions;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;
using Warehouse.Contracts.Product;
using Warehouse.Contracts.ProductPurchase;
using Warehouse.Contracts.ProductUOM;
using Warehouse.Contracts.SellProduct;
using Warehouse.Contracts.Warehouse;

namespace Application.IntergrationTests.ProductPurchases
{
    public class Purchases : TestBase
    {
        [Test]
        public async Task CreateProductPurchase_TriggeringConcurrencyException()
        {
            // Arrange
            var warehouseId = await CreateWareHouse();
            await CreateProduct();

            CreateProductPurchaseRequest productPurchaseRequest = new()
            {
                VendorName = "Some vendor",
                Comment = "Fresh fruit",
                Date = DateTime.Now,
                Price = 8,
                ProductName = "Pineapple",
                ProductUom = "kilogram",
                Quantity = 1,
                WareHouseId = warehouseId
            };
            var ctx1 = _scopeFactory.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var prod1 = ctx1.Products.FirstOrDefault(p => p.Name == "Pineapple");
            prod1.Quantity += 2;

            var ctx2 = _scopeFactory.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var prod2 = ctx2.Products.FirstOrDefault(p => p.Name == "Pineapple");
            prod2.Quantity += 5;

            ctx2.SaveChanges();
            Assert.Throws<DbUpdateConcurrencyException>(() => ctx1.SaveChanges());
        }

        [Test]
        public async Task GetPurchaseProduct_ShouldReturnListOfPurchaseProductFromDb()
        {
            // Arrange
            await CreatePurchaseProduct(3);
            GetProductPurchasesQuery request = new();

            // Act
            List<SingleProductPurchaseResponse> PurchaseProduct = await _httpClient.GetFromJsonAsync<List<SingleProductPurchaseResponse>>("PurchaseProduct", request);

            PurchaseProduct.Count().Should().BeGreaterThanOrEqualTo(3);
        }

        [Test]
        public async Task DeleteProductSell_ShouldDeleteFromDb()
        {
            await CreatePurchaseProduct(1);
            // Arrange
            var PurchaseProductFromDb = GetEntities<ProductPurchase>().First();

            // Act
            HttpResponseMessage result = await _httpClient.DeleteAsync($"/PurchaseProduct/{PurchaseProductFromDb.Id}");

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.NoContent);

            var deletedWareHouse = await GetEntity<ProductPurchase>(s => s.Id == PurchaseProductFromDb.Id);
            deletedWareHouse.Should().BeNull();
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

        async Task<Product> CreateProduct()
        {
            Product product = new()
            {
                Category = new Category() { Name = "Fruit" },
                Description = "Test",
                Id = Guid.NewGuid(),
                Manufacturer = "Microsoft",
                Name = "Pineapple",
                Price = 12,
                Quantity = 15,
                Uom = new ProductUOM() { Name = "kilogram", Abbreviation = "KG", Details = "Test", Id = Guid.NewGuid() }
            };
            await AddAsync(product);
            return product;
        }

        async Task CreatePurchaseProduct(int v)
        {
            for (int i = 0; i < v; i++)
            {
                var warehouseId = await CreateWareHouse();
                ProductPurchase PurchaseProduct = new()
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
                await AddAsync(PurchaseProduct);
            }
        }
    }
}

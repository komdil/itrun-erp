using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using Warehouse.Contracts.Product;
using Warehouse.Contracts.ProductPurchase;
using Warehouse.Contracts.ProductUOM;

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
        public async Task CreateProductPurchase_PurchasingExistingProduct_Success()
        {
            var warehouseId = await CreateWareHouse();
            var prod = await CreateProduct();

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
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/purchaseProduct", productPurchaseRequest);
            response.EnsureSuccessStatusCode();

            var product = await GetEntity<Product>(p => p.Name == "Pineapple");
            Assert.That(product.Quantity == prod.Quantity + productPurchaseRequest.Quantity);
        }

        [Test]
        public async Task CreateProductPurchase_PurchasingNewProduct_Success()
        {
            var warehouseId = await CreateWareHouse();

            CreateProductPurchaseRequest productPurchaseRequest = new()
            {
                VendorName = "Some vendor",
                Comment = "Fresh fruit",
                Date = DateTime.Now,
                Price = 8,
                ProductName = "Peach",
                ProductUom = "kilogram",
                Quantity = 1,
                WareHouseId = warehouseId
            };
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/purchaseProduct", productPurchaseRequest);
            response.EnsureSuccessStatusCode();

            var product = await GetEntity<Product>(p => p.Name == "Peach");
            Assert.That(product.Quantity == productPurchaseRequest.Quantity);
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
    }
}

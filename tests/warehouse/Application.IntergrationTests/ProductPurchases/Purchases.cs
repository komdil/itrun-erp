using Domain.Entities;
using Infrastructure.Data;
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
        public async Task CreateProductPurchase_TriggeringConcurrencyException_IsBadRequest()
        {
            // Arrange
            var warehouseId = await CreateWareHouse();
            await CreateProduct();

            CreateProductPurchaseRequest productPurchaseRequest = new()
            {
                VendorName = "Test",
                Comment = "Test",
                Date = DateTime.Now,
                Price = 8,
                ProductName = "Apple",
                ProductUom = "kilogram",
                Quantity = 1,
                WareHouseId = warehouseId
            };
            var ctx1 = _scopeFactory.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var prod1 =  ctx1.Products.FirstOrDefault(p => p.Name == "Apple");
            prod1.Quantity += 2;

            var ctx2 = _scopeFactory.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var prod2 = ctx1.Products.FirstOrDefault(p => p.Name == "Apple");
            prod2.Quantity += 5;

            ctx2.SaveChanges();
            ctx1.SaveChanges();
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
        async Task<ProductUOM> CreateProductUom()
        {
            ProductUOM productUom = new() { Details = "Used to measure weight of the product", Abbreviation = "KG", Id = Guid.NewGuid(), Name = "kilogram" };
            await AddAsync(productUom);
            return productUom;
        }

        async Task CreateProduct()
        {
            Product product = new()
            {
                Category = "Fruit",
                Description = "Test",
                Id = Guid.NewGuid(),
                Manufacturer = "Microsoft",
                Name = "Apple",
                Price = 12,
                Quantity = 15,
                Uom = new ProductUOM() { Name = "kilogram", Abbreviation="KG", Details = "Test", Id = Guid.NewGuid() }
            };
            await AddAsync(product);
        }
    }
}

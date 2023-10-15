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

namespace Application.IntergrationTests.SallProduct
{
    public class SellProductTest : TestBase
	{
		[Test]
		public async Task SellProduct_ShouldNotFound()
		{
			// Arrange
			var warehouseId = await CreateWareHouse();
			await CreateProduct();
			CreateSellProductRequest  createSellProductRequest = new ()
			{ 
			};
			HttpResponseMessage result = await _httpClient.PostAsJsonAsync("/SaleProduct", createSellProductRequest);

			// Assert
			result.StatusCode.Should().Be(HttpStatusCode.NotFound);
		}
		[Test]
		public async Task CreateProductSell_SellProductQuantitymoreofProduct_BadRequest()
		{
			var warehouseId = await CreateWareHouse();
			var prod = await CreateProduct();

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
			HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/SaleProduct", createSellProductRequest);
			Assert.That(response.StatusCode == HttpStatusCode.BadRequest);
		}
		[Test]
		public async Task CreateProductSell_SellExistingProduct_Success()
		{
			var warehouseId = await CreateWareHouse();
			var prod = await CreateProduct();

			CreateSellProductRequest createSellProductRequest = new()
			{
				ProductName = "TestName",
				Price = 10,
				Date = DateTime.Now,
				Comment = "Test",
				ProductUom = "kilogram",
				WareHouseId = warehouseId,
				TotalPrice = 100,
				Quantity = 10,

			};
			HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/SaleProduct", createSellProductRequest);
			response.EnsureSuccessStatusCode();

			var product = await GetEntity<Product>(p => p.Name == "TestName");
			Assert.That(product.Quantity == prod.Quantity - createSellProductRequest.Quantity);
		}
		[Test]
		public async Task CreateProductSell_TriggeringConcurrencyException()
		{
			// Arrange
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
				TotalPrice = 100,
				Quantity = 10,

			};
			
			var ctx1 = _scopeFactory.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();
			var prod1 = ctx1.Products.FirstOrDefault(p => p.Name == "TestName");
			prod1.Quantity -= 2;

			var ctx2 = _scopeFactory.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();
			var prod2 = ctx2.Products.FirstOrDefault(p => p.Name == "TestName");
			prod2.Quantity -= 5;

			ctx2.SaveChanges();
			Assert.Throws<DbUpdateConcurrencyException>(() => ctx1.SaveChanges());
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
				Category = "Fruit",
				Description = "Test",
				Id = Guid.NewGuid(),
				Manufacturer = "TTTT",
				Name = "TestName",
				Price = 12,
				Quantity = 15,
				Uom = new ProductUOM() { Name = "kilogram", Abbreviation = "KG", Details = "Test", Id = Guid.NewGuid() }
			};
			await AddAsync(product);
			return product;
		}
	}
}

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

namespace Application.IntergrationTests.SallProduct
{
    public class SellProductTest : TestBase
	{
		[Test]
		public async Task SellProduct_ShouldNotFound()
		{
			await CreateProduct(3);
			CreateSellProductRequest  createSellProductRequest = new ()
			{ 
				ProductName= "Namenjnjnijnji",
				Price = 1,
				Date= DateTime.Now,
				Comment="Test",
				ProductUom="Test",
				Warehouse=Guid.Empty,
				TotalPrice=1,
				Quantity = 1,

			};
			HttpResponseMessage result = await _httpClient.PostAsJsonAsync("SellProduct", createSellProductRequest);

			// Assert
			result.StatusCode.Should().Be(HttpStatusCode.NotFound);


		}

		private async Task CreateProduct(int v)
		{
			for (int i = 0; i < v; i++)
			{
				await AddAsync(new Product
				{
					Name = $"Name{i}",
					Id = Guid.NewGuid(),
					Category = "Test",
					Description = "test",
					Manufacturer = "Test",
					Price=10+i,
					Quantity=10+i,
					Uom=new ProductUOM(){ Name = "Kilo", Abbreviation = "KG", Details = "Test" } 
				});
			}
		}
	}
}

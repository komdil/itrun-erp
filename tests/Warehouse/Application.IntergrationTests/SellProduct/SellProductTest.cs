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
using Contracts.Product;

namespace Application.IntergrationTests.SallProduct
{
	public class SellProductTest : TestBase
	{
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

			SellProductRequest SellProductRequest = new()
			{
				Id = validProduct.Id,
				Quantity = 1,
			};

			// Act
			HttpResponseMessage updateProductRequestResult = await _httpClient.PutAsJsonAsync("products", SellProductRequest);

			// Assert
			updateProductRequestResult.StatusCode.Should().Be(HttpStatusCode.OK);

			var updatedProduct = await GetEntity<Product>(prod =>
							   prod.Id == SellProductRequest.Id &&
							   prod.Quantity == prod.Quantity - SellProductRequest.Quantity);
			updatedProduct.Should().NotBeNull();
		}
	}
}

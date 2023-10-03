using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using FluentAssertions;
using Warehouse.Contracts.Warehouse;
using Warehouse.Contracts.Categories;
using System.Net.Http.Json;

namespace Application.IntergrationTests.Categories
{
    public class CategoryTests: TestBase
    {
        [Test]
        public async Task PostingCategory_ShoudbeSavedToDb()
        {
            // Arrange
            CreateCategoryRequest request = new() { Name = "MyWareHouse", Description = "Lenin 226", ParentCategory = "Test", SubCategories = "TestCat" };

            // Act
            HttpResponseMessage result = await _httpClient.PostAsJsonAsync("Categories", request);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.Created);

            var createdCategory = GetEntity<Category>(n => n.Name == request.Name &&
                               n.Description == request.Description &&
                               n.ParentCategory == request.ParentCategory &&
                               n.SubCategories == request.SubCategories);
            createdCategory.Should().NotBeNull();
        }

        [Test]
        public async Task GetCategory_ShouldReturnListOfCategoryFromDb()
        {
            // Arrange
            await CreateCategories(3);
            GetCategoryQuery request = new();

            // Act
            List<SingleCategoryResponse> categories = await _httpClient.GetFromJsonAsync<List<SingleCategoryResponse>>("categories", request);

            categories.Count().Should().BeGreaterThanOrEqualTo(3);
        }

        private async Task CreateCategories(int v)
        {
            for (int i = 0; i < v; i++)
            {
                await AddAsync(new Category
                {
                    Id = Guid.NewGuid(),
                    Description = "Test",
                    ParentCategory = "test",
                    SubCategories = "test",
                    Name = $"Name{i}"
                });
            }
        }
    }
}

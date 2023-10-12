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
            CreateCategoryRequest request = new() { Name = "MyWareHouse", Description = "Lenin 226", ParentCategoryId = Guid.Parse("")};

            // Act
            HttpResponseMessage result = await _httpClient.PostAsJsonAsync("Categories", request);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.Created);

            var createdCategory = GetEntity<Category>(n => n.Name == request.Name &&
                               n.Description == request.Description &&
                               n.ParentCategoryId == request.ParentCategoryId);
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

        [Test]
        public async Task GetSingleCategory_ShouldReturnCategoryFromDb()
        {
            // Arrange
            await CreateCategories(1);
            var categoryFromDb = GetEntities<Category>().First();

            string url = $"categories/{categoryFromDb.Id}";
            // Act
            var category = await _httpClient.GetFromJsonAsync<SingleCategoryResponse>(url);

            category.Should().NotBeNull();
            category.Name.Should().Be(categoryFromDb.Name);
            category.Description.Should().Be(categoryFromDb.Description);
            category.ParentCategory.Should().Be(categoryFromDb.ParentCategory);
            category.SubCategories.Should().Be(categoryFromDb.SubCategories);
        }

        [Test]
        public async Task UpdateCategory_ShouldUpdateDb()
        {
            // Arrange
            await CreateCategories(1);
            var categoryFromDb = GetEntities<Category>().First();
            UpdateCategoryRequest request = new() { Name = Guid.NewGuid().ToString(), Description = "Lenin 226", ParentCategory = "Test", SubCategories = "Test" };

            // Act
            HttpResponseMessage result = await _httpClient.PutAsJsonAsync($"categories/{categoryFromDb.Id}", request);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var updatedCategory = await GetEntity<Category>(s => s.Id == categoryFromDb.Id);
            updatedCategory.Name.Should().Be(request.Name);
        }

        [Test]
        public async Task DeleteCategory_ShouldDeleteFromDb()
        {
            // Arrange
            await CreateCategories(1);
            var categoryFromDb = GetEntities<Category>().First();

            // Act
            HttpResponseMessage result = await _httpClient.DeleteAsync($"categories/{categoryFromDb.Id}");

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.NoContent);

            var deletedCategory = await GetEntity<Category>(s => s.Id == categoryFromDb.Id);
            deletedCategory.Should().BeNull();
        }
    }
}

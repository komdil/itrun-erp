using AutoMapper;
using Domain.Entities;
using Warehouse.Contracts.Categories;

namespace Application.Common.Mappings.Categories
{
    public class CategoryMapProfile : Profile
    {
        public CategoryMapProfile()
        {
            CreateMap<Category, SingleCategoryResponse>();
            CreateMap<CreateCategoryRequest, Category>();
        }
    }
}

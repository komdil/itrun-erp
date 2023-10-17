using AutoMapper;
using Domain.Entities;
using Warehouse.Contracts.Categories;

namespace Application.Common.Mappings.Categories
{
    public class CategoryMapProfile : Profile
    {
        public CategoryMapProfile()
        {
            CreateMap<Category, SingleCategoryResponse>()
                .ForMember(s => s.ParentCategory, x => x.MapFrom(s => s.ParentCategory.Name));
            CreateMap<CreateCategoryRequest, Category>();
        }
    }
}

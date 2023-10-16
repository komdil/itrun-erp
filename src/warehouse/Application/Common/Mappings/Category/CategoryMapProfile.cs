using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Warehouse.Contracts.Categories;
using Warehouse.Contracts.Warehouse;

namespace Application.Common.Mappings.Categories
{
    public class CategoryMapProfile : Profile
    {
        public CategoryMapProfile()
        {
            CreateMap<Category, SingleCategoryResponse>();
        }
    }
}

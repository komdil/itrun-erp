using AutoMapper;
using Warehouse.Contracts.ProductUOM;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Mappings
{
    public class ProductUomMappingProfile : Profile
    {
        public ProductUomMappingProfile() 
        {
            CreateMap<CreateProductUOMRequest, ProductUOM>()
                .ForMember(prodUom => prodUom.Id,
                opt => Guid.NewGuid());

            CreateMap<ProductUOM, CreateProductUOMResponse>()
                .ForMember(prodUomResponse => prodUomResponse.Slug,
                opt => opt.MapFrom(p =>p.Abbreviation));

            CreateMap<UpdateProductUomRequest, ProductUom>();
        }
    }
}

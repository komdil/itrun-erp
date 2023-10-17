using AutoMapper;
using Warehouse.Contracts.Product;
using Domain.Entities;

namespace Application.Common.Mappings.ProductMap
{
    public class ProductMapProfile : Profile
    {
        public ProductMapProfile()
        {
            CreateMap<CreateProductRequest, Product>();

            CreateMap<Product, SingleProductResponse>()
                .ForMember(getProduct => getProduct.ProductUom,
                    opt => opt.MapFrom(product => product.Uom.Name))
                .ForMember(getProduct => getProduct.Category,
                    opt => opt.MapFrom(product => product.Category.Name))
                .ForMember(getProduct => getProduct.Warehouse,
                    opt => opt.MapFrom(product => product.Warehouse.Name));

            //CreateMap<UpdateProductRequest, Product>();
        }
    }
}

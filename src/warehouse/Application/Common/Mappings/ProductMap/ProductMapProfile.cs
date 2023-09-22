using AutoMapper;
using Warehouse.Contracts.Product;
using Domain.Entities;

namespace Application.Common.Mappings.ProductMap
{
    public class ProductMapProfile : Profile
    {
        public ProductMapProfile()
        {
            CreateMap<CreateProductRequest, Product>()
                .ForMember(product => product.Id, opt => Guid.NewGuid());

            CreateMap<Product, SingleProductResponse>()
                .ForMember(getProduct => getProduct.ProductUom,
                    opt => opt.MapFrom(product => product.Uom.Name));

            CreateMap<UpdateProductRequest, Product>();
        }
    }
}

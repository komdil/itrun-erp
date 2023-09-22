using AutoMapper;
using Warehouse.Contracts.Product;
using Domain.Entities;

namespace Application.Common.Mappings.ProductMap
{
    public class GetProductListMapProfile : Profile
    {
        public GetProductListMapProfile()
        {
            CreateMap<Product, GetProductListQueryResponse>()
                .ForMember(getProduct => getProduct.Id,
                    opt => opt.MapFrom(product => product.Id))
                .ForMember(getProduct => getProduct.Name,
                    opt => opt.MapFrom(product => product.Name))
                .ForMember(getProduct => getProduct.ProductUom,
                    opt => opt.MapFrom(product => product.Uom.Name))
                .ForMember(getProduct => getProduct.Category,
                    opt => opt.MapFrom(product => product.Category))
                .ForMember(getProduct => getProduct.Manufacturer,
                    opt => opt.MapFrom(product => product.Manufacturer));

        }
    }
}

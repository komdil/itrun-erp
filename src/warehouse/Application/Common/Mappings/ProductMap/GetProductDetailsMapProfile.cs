using AutoMapper;
using Contracts.Product;
using Domain.Entities;

namespace Application.Common.Mappings.ProductMap
{
    public class GetProductDetailsMapProfile : Profile
    {
        public GetProductDetailsMapProfile()
        {
            CreateMap<Product, GetProductDetailsQueryResponse>()
                .ForMember(getProduct => getProduct.Id,
                    opt => opt.MapFrom(product => product.Id))
                .ForMember(getProduct => getProduct.Name,
                    opt => opt.MapFrom(product => product.Name))
                .ForMember(getProduct => getProduct.ProductUom,
                    opt => opt.MapFrom(product => product.Uom.Name))
                .ForMember(getProduct => getProduct.Category,
                    opt => opt.MapFrom(product => product.Category))
                .ForMember(getProduct => getProduct.Manufacturer,
                    opt => opt.MapFrom(product => product.Manufacturer))
                .ForMember(getProduct => getProduct.Price,
                    opt => opt.MapFrom(product => product.Price))
                .ForMember(getProduct => getProduct.Description,
                    opt => opt.MapFrom(product => product.Description))
                .ForMember(getProduct => getProduct.Quantity,
                    opt => opt.MapFrom(product => product.Quantity));
        }
    }
}

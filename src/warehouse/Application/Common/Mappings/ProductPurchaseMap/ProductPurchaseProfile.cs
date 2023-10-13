using AutoMapper;
using Domain.Entities;
using Warehouse.Contracts.ProductPurchase;

namespace Application.Common.Mappings.ProductPurchaseMap
{
    public class ProductPurchaseProfile : Profile
    {
        public ProductPurchaseProfile()
        {
            CreateMap<CreateProductPurchaseRequest, ProductPurchase>()
                .ForMember(p => p.TotalPrice, opt => opt.MapFrom(src => src.Price * src.Quantity));

            CreateMap<ProductPurchase, SingleProductPurchaseResponse>();
        }
    }
}

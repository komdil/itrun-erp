using AutoMapper;
using Warehouse.Contracts.SellProduct;

namespace Application.Common.Mappings.ProductMap
{
	public class SellProductMapProfile : Profile
	{
        public SellProductMapProfile()
        {
			CreateMap<CreateSellProductRequest, SaleProduct>()
			  .ForMember(p => p.TotalPrice, opt => opt.MapFrom(src => src.Price * src.Quantity));

			CreateMap<SaleProduct, SingleProductSellResponse>();
		}
    }
}

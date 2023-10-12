using AutoMapper;
using Domain.Entities;
using Warehouse.Contracts.SellProduct;

namespace Application.Common.Mappings.ProductMap
{
	public class SellProductMapProfile : Profile
	{
        public SellProductMapProfile()
        {
			CreateMap<CreateSellProductRequest, SaleProduct>()
			 .ForMember(product => product.Id, opt => Guid.NewGuid());
			CreateMap<SaleProduct, SingleProductSellResponse>();

		}
    }
}

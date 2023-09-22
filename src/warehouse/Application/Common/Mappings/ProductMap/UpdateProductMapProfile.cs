using AutoMapper;
using Warehouse.Contracts.Product;
using Domain.Entities;

namespace Application.Common.Mappings.ProductMap
{
    public class UpdateProductMapProfile : Profile
    {
        public UpdateProductMapProfile()
        {
            CreateMap<UpdateProductRequest, Product>()
                .ForMember(product => product.Id, opt => Guid.NewGuid())
                .ForMember(product => product.Name, opt => opt.MapFrom(updateProduct => updateProduct.Name))
                .ForMember(product => product.Category, opt => opt.MapFrom(updateProduct => updateProduct.Category))
                .ForMember(product => product.Manufacturer, opt => opt.MapFrom(updateProduct => updateProduct.Manufacturer))
                .ForMember(product => product.Price, opt => opt.MapFrom(updateProduct => updateProduct.Price))
                .ForMember(product => product.Description, opt => opt.MapFrom(updateProduct => updateProduct.Description))
                .ForMember(product => product.Quantity, opt => opt.MapFrom(updateProduct => updateProduct.Quantity));
        }
    }
}
using AutoMapper;
using Contracts.Product;
using Domain.Entities;

namespace Application.Common.Mappings.ProductMap
{
    public class CreateProductMapProfile : Profile
    {
        public CreateProductMapProfile()
        {
            CreateMap<CreateProductRequest, Product>()
                .ForMember(product => product.Id, opt => Guid.NewGuid())
                .ForMember(product => product.Name, opt => opt.MapFrom(createProduct => createProduct.Name))
                .ForMember(product => product.Category, opt => opt.MapFrom(createProduct => createProduct.Category))
                .ForMember(product => product.Manufacturer, opt => opt.MapFrom(createProduct => createProduct.Manufacturer))
                .ForMember(product => product.Price, opt => opt.MapFrom(createProduct => createProduct.Price))
                .ForMember(product => product.Description, opt => opt.MapFrom(createProduct => createProduct.Description))
                .ForMember(product => product.Quantity, opt => opt.MapFrom(createProduct => createProduct.Quantity));
        }
    }
}

using Application.Common.Interfaces;
using AutoMapper;
using Contracts.Product;
using Domain.Entities;

namespace Application.Common.Mappings.ProductMap
{
    public class CreateProductMapProfile : Profile
    {
        IApplicationDbContext _dbContext;

        public CreateProductMapProfile(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            //CreateMap<CreateProductRequest, Product>()
            //    .ForMember(product => product.Id, opt => Guid.NewGuid())
            //    .ForMember(product => product.Uom, opt => _dbContext.ProductUOMs
            //                            .FirstOrDefault(pUom => pUom.Name == opt.MapFrom(prodRequest => prodRequest.ProductUom)));

        }
    }
}

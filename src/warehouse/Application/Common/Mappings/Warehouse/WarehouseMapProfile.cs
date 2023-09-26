using AutoMapper;
using Domain.Entities;
using Warehouse.Contracts.Warehouse;

namespace Application.Common.Mappings.ProductMap
{
    public class WarehouseMapProfile : Profile
    {
        public WarehouseMapProfile()
        {
            CreateMap<WareHouse, SingleWarehouseResponse>();
        }
    }
}

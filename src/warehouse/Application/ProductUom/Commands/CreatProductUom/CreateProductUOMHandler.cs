using Application.Common.Interfaces;
using AutoMapper;
using Warehouse.Contracts.ProductUOM;
using Warehouse.Contracts.Warehouse;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ProductUom
{
    public class CreateProductUOMHandler : IRequestHandler<CreateProductUOMRequest, CreateProductUOMResponse>
    {
        IApplicationDbContext _context;
        IMapper _mapper;
        public CreateProductUOMHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CreateProductUOMResponse> Handle(CreateProductUOMRequest request, CancellationToken cancellationToken)
        {

            var productUOM = _mapper.Map<ProductUOM>(request);
            _context.ProductUOMs.Add(productUOM);
            await _context.SaveChangesAsync();
            return _mapper.Map<CreateProductUOMResponse>(productUOM);
        }
    }
}

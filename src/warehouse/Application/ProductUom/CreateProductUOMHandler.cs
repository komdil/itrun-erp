using Application.Common.Interfaces;
using Contracts.ProductUOM;
using Contracts.Warehouse;
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
        public CreateProductUOMHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<CreateProductUOMResponse> Handle(CreateProductUOMRequest request, CancellationToken cancellationToken)
        {

            var productUOM = new ProductUOM()
            {
                Name = request.Name,
                Abbreviation = request.Abbreviation,
                Details = request.Details,
                Id = Guid.NewGuid(),
            };
            _context.ProductUOMs.Add(productUOM);
            await _context.SaveChangesAsync();
            return new CreateProductUOMResponse
            {
               
            };
        }
    }
}

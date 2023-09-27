using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Warehouse.Contracts.Product;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;

namespace Application.ProductUom.Commands.UpdateProductUom
{
    public class UpdateProductUomCommandHandler : IRequestHandler<UpdateProductUomRequest, CreatProductUOMResponse>
    {
        IApplicationDbContext _dbContext;
        IMapper _mapper;

        public UpdateProductUomCommandHandler(IApplicationDbContext dbcontext, IMapper mapper)
        {
            _dbContext = dbcontext;
            _mapper = mapper;
        }

        public async Task<CreatProductUOMResponse> Handle(UpdateProductUomRequest request, CancellationToken cancellationToken)
        { 
            var productUom = await _dbContext.ProductUOMs.FirstOrDefaultAsync(pUom => pUom.Name == request.ProductUom, cancellationToken: cancellationToken);
            if (productUom == null)
                throw new ValidationFailedException(request.ProductUom);

            product.Uom = _mapper.Map<Product>(request);
            product.Uom = productUom;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return _mapper.Map<CreatProductUOMResponse>(product);
        }
    }
}

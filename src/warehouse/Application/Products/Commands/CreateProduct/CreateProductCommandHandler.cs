using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Warehouse.Contracts.Product;
using Domain.Entities;
using MediatR;

namespace Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductRequest, string>
    {
        IApplicationDbContext _dbContext;
        IMapper _mapper;

        public CreateProductCommandHandler(IApplicationDbContext dbcontext, IMapper mapper)
        {
            _dbContext = dbcontext;
            _mapper = mapper;
        }

        public async Task<string> Handle(CreateProductRequest request, CancellationToken cancellationToken)
        {
            var productUom = _dbContext.ProductUOMs.FirstOrDefault(pUom => pUom.Name == request.ProductUom);
            if (productUom == null)
            {
                throw new NotFoundException(request.ProductUom);
            }

            var product = _mapper.Map<Product>(request);
            product.Uom = productUom;

            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync(cancellationToken);
            
            return product.Name;
        }
    }
}

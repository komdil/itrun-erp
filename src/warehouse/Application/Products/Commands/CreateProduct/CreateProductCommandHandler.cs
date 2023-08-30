using Application.Common.Interfaces;
using AutoMapper;
using Contracts.Product;
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

            // var product = _mapper.Map<Product>(request);
            var product = new Product()
            {
                Name = request.Name,
                Uom = productUom,
                Manufacturer = request.Manufacturer,
                Category = request.Category,
                Description = request.Description,
                Quantity = request.Quantity,
                Price = request.Price,
                Id = Guid.NewGuid()
            };

            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync(cancellationToken);
            
            return product.Name;
        }
    }
}

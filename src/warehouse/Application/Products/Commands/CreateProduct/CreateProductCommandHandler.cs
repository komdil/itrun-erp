using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Warehouse.Contracts.Product;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductRequest, SingleProductResponse>
    {
        IApplicationDbContext _dbContext;
        IMapper _mapper;

        public CreateProductCommandHandler(IApplicationDbContext dbcontext, IMapper mapper)
        {
            _dbContext = dbcontext;
            _mapper = mapper;
        }

        public async Task<SingleProductResponse> Handle(CreateProductRequest request, CancellationToken cancellationToken)
        {
            var productUom = await _dbContext.ProductUOMs.FirstOrDefaultAsync(pUom => pUom.Abbreviation == request.ProductUom, cancellationToken: cancellationToken);
            if (productUom == null)
                throw new ValidationFailedException("Product UOM", request.ProductUom);

            var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Name == request.Category, cancellationToken: cancellationToken);
            if (category == null)
                throw new ValidationFailedException("Category", request.Category);

            var warehouse = await _dbContext.Warehouses.FirstOrDefaultAsync(w => w.Name == request.Warehouse, cancellationToken: cancellationToken);
            if (warehouse == null)
                throw new ValidationFailedException("Warehouse", request.ProductUom);

            var product = new Product
            {
                Name = request.Name,
                Quantity = request.Quantity,
                Description = request.Description,
                Price = request.Price,
                UomId = productUom.Id,
                WarehouseId = warehouse.Id,
                CategoryId = category.Id,
            };

            await _dbContext.Products.AddAsync(product, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<SingleProductResponse>(product);
        }
    }
}

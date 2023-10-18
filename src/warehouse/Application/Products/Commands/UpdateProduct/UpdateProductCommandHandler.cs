using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Warehouse.Contracts.Product;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductRequest, SingleProductResponse>
    {
        IApplicationDbContext _dbContext;
        IMapper _mapper;

        public UpdateProductCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<SingleProductResponse> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(prod => prod.Id == request.Id, cancellationToken: cancellationToken);

            if (product == null)
                throw new NotFoundException();

            var productUom = await _dbContext.ProductUOMs.FirstOrDefaultAsync(pUom => pUom.Abbreviation == request.ProductUom, cancellationToken: cancellationToken);
            if (productUom == null)
                throw new ValidationFailedException("Product UOM", request.ProductUom);

            var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Name == request.Category, cancellationToken: cancellationToken);
            if (category == null)
                throw new ValidationFailedException("Category", request.Category);

            var warehouse = await _dbContext.Warehouses.FirstOrDefaultAsync(w => w.Name == request.Warehouse, cancellationToken: cancellationToken);
            if (warehouse == null)
                throw new ValidationFailedException("Warehouse", request.Warehouse);
            product.Name = request.Name;
            product.Manufacturer = request.Manufacturer;
            product.Price = request.Price;
            product.Description = request.Description;
            product.Quantity = request.Quantity;
            product.UomId = productUom.Id;
            product.WarehouseId = warehouse.Id;
            product.CategoryId = category.Id;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return _mapper.Map<SingleProductResponse>(product);
        }
    }
}

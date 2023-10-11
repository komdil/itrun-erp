using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Warehouse.Contracts.ProductPurchase;

namespace Application.ProductPurchases.Commands.CreateProductPurchase
{
    public class CreateProductPurchaseCommandHandler : IRequestHandler<CreateProductPurchaseRequest, SingleProductPurchaseResponse>
    {
        private readonly IApplicationDbContext _dbcontext;
        private readonly IMapper _mapper;

        public CreateProductPurchaseCommandHandler(IApplicationDbContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }
        public async Task<SingleProductPurchaseResponse> Handle(CreateProductPurchaseRequest request, CancellationToken cancellationToken)
        {
            var uom = await _dbcontext.ProductUOMs.FirstOrDefaultAsync(u => u.Name == request.ProductUom, cancellationToken);

            if (uom == null)
                throw new NotFoundException();

            var product = await _dbcontext.Products.FirstOrDefaultAsync(p => p.Name == request.ProductName, cancellationToken);

            if (product == null)
            {
                product = new Product { Name = request.ProductName, Uom = uom, Price = request.Price, Quantity = request.Quantity };
                await _dbcontext.Products.AddAsync(product);
            }
            else
            {
                product.Quantity += request.Quantity;
            }
            var prod = _mapper.Map<ProductPurchase>(request);
            var saved = false;

            while (!saved)
            {
                try
                {
                    await _dbcontext.ProductPurchases.AddAsync(prod);
                    await _dbcontext.SaveChangesAsync(cancellationToken);
                    saved = true;
                } catch (DbUpdateConcurrencyException ex)
                {
                    foreach (var item in ex.Entries)
                    {
                        if (item.Entity is ProductPurchase)
                        {
                            var currentValues = item.CurrentValues;
                            var dbValues = item.GetDatabaseValues();

                            foreach (var prop in currentValues.Properties)
                            {
                                var currentValue = currentValues[prop];
                                var dbValue = dbValues[prop];
                            }

                            item.OriginalValues.SetValues(dbValues);
                        }
                        else
                        {
                            throw new NotSupportedException("Concurrency conflict " + item.Metadata.Name);
                        }
                    }
                }
            }

            return _mapper.Map<SingleProductPurchaseResponse>(prod);
        }
    }
}

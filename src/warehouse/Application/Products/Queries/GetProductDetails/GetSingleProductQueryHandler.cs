using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Warehouse.Contracts.Product;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Queries.GetProductDetails
{
    public class GetSingleProductQueryHandler : IRequestHandler<GetSingleProductQuery, SingleProductResponse>
    {
        IApplicationDbContext _dbContext;
        IMapper _mapper;

        public GetSingleProductQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<SingleProductResponse> Handle(GetSingleProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _dbContext.Products
                .FirstOrDefaultAsync(product =>
                product.Id == request.ProductId, cancellationToken);

            if (product == null)
                throw new NotFoundException();

            return _mapper.Map<SingleProductResponse>(product);
        }
    }
}

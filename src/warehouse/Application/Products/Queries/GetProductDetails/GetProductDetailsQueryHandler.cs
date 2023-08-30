using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Contracts.Product;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Queries.GetProductDetails
{
    public class GetProductDetailsQueryHandler : IRequestHandler<GetProductDetailsQueryRequest, GetProductDetailsQueryResponse>
    {
        IApplicationDbContext _dbContext;
        IMapper _mapper;

        public GetProductDetailsQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<GetProductDetailsQueryResponse> Handle(GetProductDetailsQueryRequest request, CancellationToken cancellationToken)
        {
            var product = await _dbContext.Products
                .FirstOrDefaultAsync(product =>
                product.Id == request.ProductId, cancellationToken);

            if (product == null)
            {
                throw new NotFoundException(request.ProductId.ToString());
            }

            return _mapper.Map<GetProductDetailsQueryResponse>(product);
        }
    }
}

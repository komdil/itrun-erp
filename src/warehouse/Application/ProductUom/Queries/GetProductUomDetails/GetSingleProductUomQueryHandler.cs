using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Warehouse.Contracts.Product;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using Warehouse.Contracts.ProductUOM;

namespace Application.ProductUom.Queries.GetProductUomDetails
{
    public class GetSingleProductUomQueryHandler : IRequestHandler<GetSingleProductUomQuery, SingleProductUomResponse>
    {
        IApplicationDbContext _dbContext;
        IMapper _mapper;

        public GetSingleProductUomQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<SingleProductUomResponse> Handle(GetSingleProductUomQuery request, CancellationToken cancellationToken)
        {
            var productuom = await _dbContext.Products
                .FirstOrDefaultAsync(productuom =>
                productuom.Id == request.ProductUomId, cancellationToken);

            if (productuom == null)
                throw new NotFoundException();

            return _mapper.Map<SingleProductUomResponse>(productuom);
        }
    }
}

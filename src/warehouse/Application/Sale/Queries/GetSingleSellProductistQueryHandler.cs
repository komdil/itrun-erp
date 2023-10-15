using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Warehouse.Contracts.SellProduct;

namespace Application.Sale.Queries
{
	public class GetSingleSellProductistQueryHandler : IRequestHandler<GetSingleSaleProductsQuery, SingleProductSellResponse>
    {
        private readonly IApplicationDbContext _dbcontext;
        private readonly IMapper _mapper;

        public GetSingleSellProductistQueryHandler(IApplicationDbContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }

        public async Task<SingleProductSellResponse> Handle(GetSingleSaleProductsQuery request, CancellationToken cancellationToken)
        {
			SaleProduct warehouse = await _dbcontext.SaleProducts.FirstOrDefaultAsync(s => s.Id == request.Id);
			if (warehouse == null)
				throw new NotFoundException();
			return _mapper.Map<SingleProductSellResponse>(warehouse);
		}
    }
}

using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Warehouse.Contracts.SellProduct;
using Warehouse.Contracts.Warehouse;

namespace Application.Sale.Queries
{
	public class GetSaleProductsQueryHandler : IRequestHandler<GetSaleProductsQuery, List<SingleProductSellResponse>>
	{
		IApplicationDbContext _dbContext;
		IMapper _mapper;

		public GetSaleProductsQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}
		public async Task<List<SingleProductSellResponse>> Handle(GetSaleProductsQuery request, CancellationToken cancellationToken)
		{
			IQueryable<SaleProduct> SaleProduct = _dbContext.SaleProducts;
			if (!string.IsNullOrWhiteSpace(request.ProductName))
				SaleProduct = SaleProduct.Where(p => p.ProductName == request.ProductName);

			if (!string.IsNullOrWhiteSpace(request.ProductUom))
				SaleProduct = SaleProduct.Where(p => p.ProductUom == request.ProductUom);


			SaleProduct = SaleProduct.Take(request.PageSize);

			return await SaleProduct.ProjectTo<SingleProductSellResponse>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
		}
	}
}

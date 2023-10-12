using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Contracts.SellProduct;

namespace Application.Sale
{
	public class CreateProductPurchaseCommandHandler : IRequestHandler<CreateSellProductRequest, SingleProductSellResponse>
	{
		private readonly IApplicationDbContext _dbcontext;
		private readonly IMapper _mapper;

		public CreateProductPurchaseCommandHandler(IApplicationDbContext dbcontext, IMapper mapper)
		{
			_dbcontext = dbcontext;
			_mapper = mapper;
		}

		public async Task<SingleProductSellResponse> Handle(CreateSellProductRequest request, CancellationToken cancellationToken)
		{
			var product = await _dbcontext.Products.FirstOrDefaultAsync(p => p.Name == request.ProductName, cancellationToken);
			if (product == null)
				throw new NotFoundException();
			if (product.Quantity < request.Quantity)
				throw new ValidationFailedException(request.ProductName);

			product.Quantity -= request.Quantity;
			var prod = _mapper.Map<SingleProductSellResponse>(request);

			await _dbcontext.SaveChangesAsync();

			return _mapper.Map<SingleProductSellResponse>(prod);
		}
	}
}

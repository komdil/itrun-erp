using Application.Common.Interfaces;
using AutoMapper;
using Contracts.Product;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.Commands.UpdateProduct
{
	public class SellProductCommandHandler : IRequestHandler<SellProductRequest, bool>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public SellProductCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
			_context = context;
			_mapper = mapper;
		}
        public async Task<bool> Handle(SellProductRequest request, CancellationToken cancellationToken)
		{
			var prod = await _context.Products.FirstOrDefaultAsync(p => p.Id == request.Id);
			if (request.Quantity > prod.Quantity)
			{
				return false;
			}

			prod.Quantity -= request.Quantity;

			var sale = _mapper.Map<SaleProduct>(prod);
			_context.SaleProducts.Add(sale);

			await _context.SaveChangesAsync();

			return true;
		}
	}
}

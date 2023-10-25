using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Warehouse.Contracts.ProductPurchase;
using Warehouse.Contracts.Reports;
using Warehouse.Contracts.SellProduct;

namespace Application.Reports
{
    public class GetProductAggregatesQueryHandler : IRequestHandler<GetProductAggregatesQuery, ProductAggregatesResponse>
    {
        IApplicationDbContext _context;
        IMapper _mapper;
        public GetProductAggregatesQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _context = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<ProductAggregatesResponse> Handle(GetProductAggregatesQuery request, CancellationToken cancellationToken)
        {
            var sumOfSales = await _context.SaleProducts.SumAsync(s => s.Quantity, cancellationToken);
            var sumOfPurchases = await _context.ProductPurchases.SumAsync(s => s.Quantity, cancellationToken);
            var customerCount = await _context.SaleProducts.CountAsync(cancellationToken);
            var sumOfProducts = await _context.Products.SumAsync(s => s.Quantity, cancellationToken);
            var totalExpenses = await _context.ProductPurchases.SumAsync(s => s.Price, cancellationToken);
            var totalRevanue = await _context.SaleProducts.SumAsync(s => s.Price, cancellationToken);
            var profit = totalRevanue - totalExpenses;

            var mostPurchases = await _context.ProductPurchases
                .GroupBy(s => s.ProductName)
                .OrderByDescending(a => a.Sum(s => s.TotalPrice))
                .Select(s => new
                {
                    Purchase = s.First(),
                    Quantity = s.Sum(s => s.Quantity),
                    TotalPrice = s.Sum(s => s.TotalPrice)
                })
                .Take(10)
                .ToListAsync(cancellationToken);
            mostPurchases.ForEach(s =>
            {
                s.Purchase.Quantity = s.Quantity;
                s.Purchase.TotalPrice = s.TotalPrice;
            });

            var mostSellers = await _context.SaleProducts
            .GroupBy(s => s.ProductName)
            .OrderByDescending(a => a.Sum(s => s.TotalPrice))
            .Select(s => new
            {
                ProductSale = s.First(),
                Quantity = s.Sum(s => s.Quantity),
                TotalPrice = s.Sum(s => s.TotalPrice)
            })
            .Take(10)
            .ToListAsync(cancellationToken);
            mostSellers.ForEach(s =>
            {
                s.ProductSale.Quantity = s.Quantity;
                s.ProductSale.TotalPrice = s.TotalPrice;
            });

            return new ProductAggregatesResponse
            {
                CustomersCount = customerCount,
                ProductsCount = sumOfProducts,
                PurchasesSumm = sumOfPurchases,
                SalesSum = sumOfProducts,
                Expenses = totalExpenses,
                Revenue = totalRevanue,
                Profit = profit,
                MostPurchases = _mapper.Map<List<SingleProductPurchaseResponse>>(mostPurchases.Select(s => s.Purchase).ToList()),
                MostSellers = _mapper.Map<List<SingleProductSellResponse>>(mostSellers.Select(s => s.ProductSale).ToList())
            };
        }
    }
}

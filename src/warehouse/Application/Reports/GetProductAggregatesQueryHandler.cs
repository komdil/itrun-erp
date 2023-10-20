using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Warehouse.Contracts.Reports;

namespace Application.Reports
{
    public class GetProductAggregatesQueryHandler : IRequestHandler<GetProductAggregatesQuery, ProductAggregatesResponse>
    {
        IApplicationDbContext _context;
        public GetProductAggregatesQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
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
            return new ProductAggregatesResponse
            {
                CustomersCount = customerCount,
                ProductsCount = sumOfProducts,
                PurchasesSumm = sumOfPurchases,
                SalesSum = sumOfProducts,
                Expenses = totalExpenses,
                Revenue = totalRevanue,
                Profit = profit
            };
        }
    }
}

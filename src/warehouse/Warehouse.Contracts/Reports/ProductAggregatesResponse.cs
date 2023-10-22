using Warehouse.Contracts.ProductPurchase;
using Warehouse.Contracts.SellProduct;

namespace Warehouse.Contracts.Reports
{
    public record ProductAggregatesResponse
    {
        public int PurchasesSumm { get; set; }
        public int SalesSum { get; set; }
        public int ProductsCount { get; set; }
        public int CustomersCount { get; set; }

        public decimal Revenue { get; set; }

        public decimal Profit { get; set; }

        public decimal Expenses { get; set; }

        public List<SingleProductPurchaseResponse> MostPurchases { get; set; }

        public List<SingleProductSellResponse> MostSellers { get; set; }
    }
}

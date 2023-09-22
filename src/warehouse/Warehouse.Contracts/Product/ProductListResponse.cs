namespace Warehouse.Contracts.Product
{
    public record ProductListResponse
    {
        public IList<GetProductListQueryResponse> ProductsList { get; init; }   
    }
}

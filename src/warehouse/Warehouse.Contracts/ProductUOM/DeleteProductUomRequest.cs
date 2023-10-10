using MediatR;

namespace Warehouse.Contracts.ProductUOM
{
    public record DeleteProductUomRequest(string Slug) : IRequest;
}

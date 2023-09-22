using MediatR;

namespace Warehouse.Contracts.Product
{
    public record DeleteProductRequest(string Slug) : IRequest;
}

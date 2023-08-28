using MediatR;

namespace Contracts.Product
{
    public record DeleteProductRequest(string Slug) : IRequest;
}

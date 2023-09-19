using Contracts.Product;
using FluentValidation;

namespace Application.Products.Queries.GetProductDetails
{
    public class GetProductDetailsValidator : AbstractValidator<GetProductDetailsQueryRequest>
    {
        public GetProductDetailsValidator()
        {
            RuleFor(product => product.ProductId).NotNull();
        }
    }
}

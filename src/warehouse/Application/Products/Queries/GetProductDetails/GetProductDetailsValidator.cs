using Warehouse.Contracts.Product;
using FluentValidation;

namespace Application.Products.Queries.GetProductDetails
{
    public class GetProductDetailsValidator : AbstractValidator<GetSingleProductQuery>
    {
        public GetProductDetailsValidator()
        {
            RuleFor(product => product.ProductId).NotNull();
        }
    }
}

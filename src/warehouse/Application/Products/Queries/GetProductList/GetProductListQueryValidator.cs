using Contracts.Product;
using FluentValidation;

namespace Application.Products.Queries.GetProductList
{
    public class GetProductListQueryValidator : AbstractValidator<GetProductListQueryRequest>
    {
        public GetProductListQueryValidator()
        {

        }
    }
}

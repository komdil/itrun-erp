using Warehouse.Contracts.Product;
using FluentValidation;

namespace Application.ProductUom.Queries.GetProductUomDetails
{
    public class GetProductUomDetailsValidator : AbstractValidator<GetSingleProductUomQuery>
    {
        public GetProductUomDetailsValidator()
        {
            RuleFor(productuom => productuom.ProductUomId).NotNull();
        }
    }
}

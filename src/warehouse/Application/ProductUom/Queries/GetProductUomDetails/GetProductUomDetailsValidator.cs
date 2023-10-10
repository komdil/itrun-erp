using Warehouse.Contracts.Product;
using FluentValidation;
using Warehouse.Contracts.ProductUOM;

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

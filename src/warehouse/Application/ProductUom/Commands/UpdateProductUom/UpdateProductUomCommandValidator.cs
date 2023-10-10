using Warehouse.Contracts.Product;
using FluentValidation;

namespace Application.ProductUom.Commands.UpdateProductUom
{
    public class UpdateProductUomCommandValidator : AbstractValidator<UpdateProductUomRequest>
    {
        public UpdateProductUomCommandValidator()
        {
            RuleFor(productuom => productuom.Name).NotEmpty();
            RuleFor(productuom => productuom.Id).NotEmpty();
        }
    }
}

using Warehouse.Contracts.Product;
using FluentValidation;

namespace Application.ProductUom.Commands.DeleteProduct
{
    public class DeleteProductUomCommandValidator : AbstractValidator<DeleteProductUomRequest>
    {
        public DeleteProductUomCommandValidator()
        {
            RuleFor(productuom => productuom.Slug).NotEmpty();
        }
    }
}

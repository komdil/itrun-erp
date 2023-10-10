using Warehouse.Contracts.Product;
using FluentValidation;
using Warehouse.Contracts.ProductUOM;

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

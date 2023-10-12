using Warehouse.Contracts.Product;
using FluentValidation;

namespace Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(product => product.Name).NotEmpty();
            RuleFor(product => product.Id).NotEmpty();
        }
    }
}

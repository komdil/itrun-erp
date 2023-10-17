using Warehouse.Contracts.Product;
using FluentValidation;

namespace Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductRequest>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(product => product.Name).NotEmpty();
            RuleFor(product => product.Category).NotEmpty();
            RuleFor(product => product.Warehouse).NotEmpty();
        }
    }
}

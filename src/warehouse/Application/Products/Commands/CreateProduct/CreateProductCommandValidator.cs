using Warehouse.Contracts.Product;
using FluentValidation;

namespace Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductRequest>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(product => product.Name).NotEmpty();
        }
    }
}

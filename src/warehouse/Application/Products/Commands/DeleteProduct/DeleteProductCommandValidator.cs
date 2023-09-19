using Contracts.Product;
using FluentValidation;

namespace Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductRequest>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(product => product.Slug).NotEmpty();
        }
    }
}

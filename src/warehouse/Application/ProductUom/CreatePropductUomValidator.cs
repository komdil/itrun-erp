using System;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Contracts.ProductUOM;

namespace Application.ProductUom
{
    public class CreatePropductUomValidator : AbstractValidator<CreateProductUOMRequest>
    {
        public CreatePropductUomValidator()
        {
            RuleFor(productUom => productUom.Name).NotEmpty();
        }
    }
}

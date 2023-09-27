using Warehouse.Contracts.Product;
using FluentValidation;

namespace Application.ProductUom.Queries.GetProductUomList
{
    public class GetProductUomListQueryValidator : AbstractValidator<GetProductUomQuery>
    {
        public GetProductUomListQueryValidator()
        {

        }
    }
}

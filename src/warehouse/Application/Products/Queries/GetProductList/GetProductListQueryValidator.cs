﻿using Warehouse.Contracts.Product;
using FluentValidation;

namespace Application.Products.Queries.GetProductList
{
    public class GetProductListQueryValidator : AbstractValidator<GetProductsQuery>
    {
        public GetProductListQueryValidator()
        {

        }
    }
}

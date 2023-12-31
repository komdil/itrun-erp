﻿using MediatR;

namespace Warehouse.Contracts.Product
{
    public record UpdateProductRequest : IRequest<SingleProductResponse>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ProductUom { get; set; }
        public string Category { get; set; }
        public string Warehouse { get; set; }
        public string Manufacturer { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
    }
}

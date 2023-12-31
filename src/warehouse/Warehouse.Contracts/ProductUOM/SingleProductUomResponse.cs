﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Contracts.ProductUOM
{
    public record SingleProductUomResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public string Abbreviation { get; set; }
        public string Slug { get; init; }
    }
}

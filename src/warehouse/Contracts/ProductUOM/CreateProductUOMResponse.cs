using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ProductUOM
{
    public record CreateProductUOMResponse
    {
        public string Name { get; set; }
        public string Details { get; set; }
        public string Location { get; set; }
        public string Slug { get; init; }
    }
}

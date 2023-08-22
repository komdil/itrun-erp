using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Warehouse
{
    public record CreateWarehouseResponse
    {
        public string Slug { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public string Location { get; set; }
    }
}

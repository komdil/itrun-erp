using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Contracts.Dtos
{
    public class WareHouse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }

        public string Location { get; set; }

    }
}

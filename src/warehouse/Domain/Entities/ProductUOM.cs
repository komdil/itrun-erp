using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductUOM
    {
        public string Name { get; set; }
        public string Details { get; set; }
        public string Abbreviation { get; set; }
        public Guid Id { get; set; }
    }
}

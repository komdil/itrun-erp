using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Exceptions
{
    public record ErrorMessage
    {
        public string PropertyName { get; init; }
        public string Message { get; init; }
    }
}

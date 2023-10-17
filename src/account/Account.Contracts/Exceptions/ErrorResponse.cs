using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Contracts.Exceptions
{
    public record ErrorResponse
    {
        public List<ErrorMessage> Errors { get; init; }
    }
}

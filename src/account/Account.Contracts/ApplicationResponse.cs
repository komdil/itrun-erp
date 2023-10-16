using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public abstract class ApplicationResponse<TResponse>
    {
        public TResponse? Response { get; protected set; }
        public bool IsSuccess { get; protected set; } = true;
        public string? ErrorMessage { get; protected set; }
   
    }

    public abstract class ApplicationResponse
    {
        public bool IsSuccess { get; protected set; } = true;
        public string? ErrorMessage { get; protected set; }
     
    }
}

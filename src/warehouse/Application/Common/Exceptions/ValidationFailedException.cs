using Warehouse.Contracts.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public class ValidationFailedException : Exception
    {
        public ErrorResponse ErrorResponse { get; }
        public ValidationFailedException(ErrorResponse errorResponse) 
            : base(string.Join(',', errorResponse.Errors.Select(err=> err.Message))) 
        {
            ErrorResponse = errorResponse;
        }
    }
}

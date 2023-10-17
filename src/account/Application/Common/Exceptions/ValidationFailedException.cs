using Account.Contracts.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Application.Common.Exceptions
{
    public class ValidationFailedException : Exception
    {
        public ErrorResponse ErrorResponse { get; }
        public ValidationFailedException(ErrorResponse errorResponse)
            : base(string.Join(',', errorResponse.Errors.Select(err => err.Message)))
        {
            ErrorResponse = errorResponse;
        }

        public ValidationFailedException(string errorMessage)
         : base(errorMessage)
        {
            ErrorResponse = new ErrorResponse
            {
                Errors = new List<ErrorMessage>
                {
                    new ErrorMessage
                    {
                        Message=errorMessage
                    }
                }
            };
        }

        public ValidationFailedException(IEnumerable<IdentityError> errors)
        {
            ErrorResponse = new ErrorResponse
            {
                Errors = errors.Select(s => new ErrorMessage
                {
                    Message = s.Description
                }).ToList()
            };
        }
    }
}

using Warehouse.Contracts.Exceptions;

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

        public ValidationFailedException(string notFoundEntityName)
         : base($"Entity \"{notFoundEntityName}\" not found.")
        {

            ErrorResponse = new ErrorResponse
            {
                Errors = new List<ErrorMessage>
                {
                    new ErrorMessage
                    {
                        Message=$"Entity \"{notFoundEntityName}\" not found."
                    }
                }
            };
        }
    }
}

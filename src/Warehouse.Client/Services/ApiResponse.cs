using System.Net;

namespace Warehouse.Client.Services
{
    public record ApiResponse<T>
    {
        public bool Success { get; set; }

        public string Error { get; set; }

        public T Response { get; set; }

        public HttpStatusCode? HttpStatusCode { get; set; }

        public static ApiResponse<T> BuildSuccess(T response, HttpStatusCode? httpStatusCode = null)
        {
            return new ApiResponse<T>
            {
                Success = true,
                Response = response,
                HttpStatusCode = httpStatusCode
            };
        }

        public static ApiResponse<T> BuildFailed(string error, HttpStatusCode? httpStatusCode = null)
        {
            return new ApiResponse<T>
            {
                Error = error,
                HttpStatusCode = httpStatusCode,
            };
        }

        public static ApiResponse<T> BuildFailed(T response, HttpStatusCode? httpStatusCode = null)
        {
            return new ApiResponse<T>
            {
                HttpStatusCode = httpStatusCode,
                Response = response
            };
        }
    }
}

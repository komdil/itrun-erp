using Application.Behaviours.Exceptions;

namespace Account.Api
{
    public class ExceptionHandlerMiddleware
    {
        RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (AccountValidationFailureException ex)
            {
                context.Response.Clear();
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(ex);
            }
        }
    }
}

using System;
using System.Threading.Tasks;

namespace Warehouse.Api.Middlewares
{
	public class AuthMiddleware
	{
        private readonly RequestDelegate _next;
        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        const string userName = "Username";
        const string userNameValue = "Admin";

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);
        }
    }
}

using Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Warehouse.Contracts.Exceptions;

namespace Warehouse.Api.Filters
{
    public class ApiValidationFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is ValidationFailedException exception)
            {
                context.ExceptionHandled = true;
                context.Result = new BadRequestObjectResult(exception.ErrorResponse);
                return;
            }
            else if (context.Exception is NotFoundException)
            {
                context.ExceptionHandled = true;
                context.Result = new NotFoundResult();
                return;
            }
            else if (context.Exception is DbUpdateConcurrencyException)
            {
                context.ExceptionHandled = true;
                context.Result = new BadRequestObjectResult(new ErrorResponse()
                {
                    Errors = new List<ErrorMessage> { new ErrorMessage() { Message = "Something went wrong, try again." } }
                });
            }
            base.OnException(context);
        }
    }
}

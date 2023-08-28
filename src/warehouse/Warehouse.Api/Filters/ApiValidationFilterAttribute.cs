﻿using Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Warehouse.Api.Filters
{
    public class ApiValidationFilterAttribute: ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if(context.Exception is ValidationFailedException exception)
            {
                context.ExceptionHandled = true;
                context.Result = new BadRequestObjectResult(exception.ErrorResponse);
                return;
            }

            base.OnException(context);
        }
    }
}
using Application.Behaviours.Exceptions;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Behaviours
{
    public class ValidationBehaviour<IRequest, IResponse> : IPipelineBehavior<IRequest, IResponse>
    {
        IValidator<IRequest> _validator;
        public ValidationBehaviour(IValidator<IRequest> validator)
        {
            _validator = validator;
        }

        public async Task<IResponse> Handle(IRequest request, RequestHandlerDelegate<IResponse> next, CancellationToken cancellationToken)
        {
            var result = await _validator.ValidateAsync(request);
            if (!result.IsValid)
            {
                throw new AccountValidationFailureException(string.Join(',', result.Errors.Select(s => s.ErrorMessage)));
            }
            return await next();
        }
    }
}

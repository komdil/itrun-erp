using Contracts.Requests.Admin;
using FluentValidation;
using MediatR;

namespace Application.Admin
{
    public class AssignRoleCommandHandler : IRequestHandler<AssignRoleCommand, bool>
    {
        IValidator<AssignRoleCommand> _validator;
        public AssignRoleCommandHandler(IValidator<AssignRoleCommand> validator)
        {
            _validator = validator;
        }

        public async Task<bool> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
        {
            var result = _validator.Validate(request);
            if (!result.IsValid)
            {
                return false;
            }
            return true;
        }
    }
}

using Contracts.Requests.Admin;
using FluentValidation;
using MediatR;

namespace Application.Admin
{
    public class AssignRoleCommandHandler : IRequestHandler<AssignRoleCommand, bool>
    {
        public async Task<bool> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
        {
            return true;
        }
    }
}

using MediatR;

namespace Contracts.Requests.Admin
{
    public record AssignRoleCommand : IRequest<bool>
    {
        public string Role { get; init; }

        public Guid UserId { get; init; }
    }
}

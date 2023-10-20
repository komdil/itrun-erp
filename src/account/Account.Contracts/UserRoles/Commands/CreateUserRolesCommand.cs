using Account.Contracts.UserRoles.Responses;
using MediatR;

namespace Account.Contracts.UserRoles.Commands
{
    public class CreateUserRolesCommand : IRequest<UserRolesResponse>
    {
        public Guid RoleId { get; set; }
        public Guid UserId { get; set; }
    }
}

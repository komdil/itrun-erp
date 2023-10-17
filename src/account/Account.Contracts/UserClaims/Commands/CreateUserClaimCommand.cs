using Account.Contracts.Claims.Responses;
using MediatR;

namespace Account.Contracts.Claims.Commands
{
    public class CreateUserClaimCommand : IRequest<SingleClaimResponse>
    {
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }

        public Guid UserId { get; set; }
    }
}

using Account.Contracts.Claims.Responses;
using MediatR;

namespace Account.Contracts.Claims.Commands
{
    public class UpdateUserClaimCommand : IRequest<SingleClaimResponse>
    {
        public string ClaimValue { get; set; }
        public string Slug { get; set; }
    }
}

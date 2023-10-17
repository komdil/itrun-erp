using Account.Contracts.Claims.Responses;
using MediatR;

namespace Account.Contracts.Claims.Queries
{
    public class GetUserClaimsQuery : IRequest<List<SingleClaimResponse>>
    {
        public string UserName { get; set; }
        public string ClaimType { get; set; }
    }
}

using MediatR;

namespace Account.Contracts.Claims.Commands
{
    public class DeleteUserClaimCommand : IRequest
    {
        public string Slug { get; set; }
    }
}

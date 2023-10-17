using MediatR;
using Application.Common;
using Microsoft.EntityFrameworkCore;
using Application.Common.Exceptions;
using Account.Contracts.Claims.Commands;

namespace Application.UserRoles
{
    public class DeleteUserClaimCommandHandler : IRequestHandler<DeleteUserClaimCommand>
    {
        private readonly IApplicationDbContext _context;
        public DeleteUserClaimCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteUserClaimCommand request, CancellationToken cancellationToken)
        {
            var userClaim = await _context.UserClaims.FirstOrDefaultAsync(ur => ur.Slug == request.Slug, cancellationToken);
            if (userClaim == null)
                throw new NotFoundException();

            _context.UserClaims.Remove(userClaim);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

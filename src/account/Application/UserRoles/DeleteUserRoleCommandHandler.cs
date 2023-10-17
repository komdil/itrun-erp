using Account.Contracts.UserRoles.Commands;
using MediatR;
using Application.Common;
using Microsoft.EntityFrameworkCore;
using Application.Common.Exceptions;

namespace Application.UserRoles
{
    public class DeleteUserRoleCommandHandler : IRequestHandler<DeleteUserRoleCommand>
    {
        private readonly IApplicationDbContext _context;
        public DeleteUserRoleCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteUserRoleCommand request, CancellationToken cancellationToken)
        {
            var userRole = await _context.UserRoles.FirstOrDefaultAsync(ur => ur.Slug == request.Slug, cancellationToken);
            if (userRole == null)
                throw new NotFoundException();

            _context.UserRoles.Remove(userRole);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

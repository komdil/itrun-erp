using Account.Contracts.UserRoles.Queries;
using Account.Contracts.UserRoles.Responses;
using MediatR;
using Application.Common;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Application.UserRoles
{
    public class GetUserRolesQueryHandler : IRequestHandler<GetUserRolesQuery, List<UserRolesResponse>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetUserRolesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<UserRolesResponse>> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
        {
            var superUser = await _context.Users.SingleAsync(s => s.UserName == Constants.SuperAdminUserName, cancellationToken);
            return await _mapper.ProjectTo<UserRolesResponse>(_context.UserRoles.Where(s => s.UserId != superUser.Id)).ToListAsync(cancellationToken);
        }
    }
}

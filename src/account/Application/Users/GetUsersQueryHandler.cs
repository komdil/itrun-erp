using MediatR;
using Application.Common;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Account.Contracts.User.Queries;
using Account.Contracts.User.Response;

namespace Application.UserRoles
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<UserResponse>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetUsersQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<UserResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            return await _mapper.ProjectTo<UserResponse>(_context.Users.Where(s => s.UserName != Constants.SuperAdminUserName)).ToListAsync(cancellationToken);
        }
    }
}

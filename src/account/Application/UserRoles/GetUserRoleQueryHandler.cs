using Application.Common;
using Application.Common.Exceptions;
using AutoMapper;
using Account.Contracts.UserRoles.Queries;
using Account.Contracts.UserRoles.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UserRoles
{
    public class GetUserRoleQueryHandler : IRequestHandler<GetUserRoleQuery, UserRolesResponse>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        public GetUserRoleQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<UserRolesResponse> Handle(GetUserRoleQuery request, CancellationToken cancellationToken)
        {
            var userRole = await _applicationDbContext.UserRoles.FirstOrDefaultAsync(s => s.Slug == request.Slug, cancellationToken);
            if (userRole == null)
                throw new NotFoundException();
            return _mapper.Map<UserRolesResponse>(userRole);
        }
    }
}

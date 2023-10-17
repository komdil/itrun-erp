using Application.Common;
using AutoMapper;
using Account.Contracts.Claims.Queries;
using Account.Contracts.Claims.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UserRoles
{
    public class GetUserClaimsQueryHandler : IRequestHandler<GetUserClaimsQuery, List<SingleClaimResponse>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        public GetUserClaimsQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public Task<List<SingleClaimResponse>> Handle(GetUserClaimsQuery request, CancellationToken cancellationToken)
        {
            return _mapper.ProjectTo<SingleClaimResponse>(_applicationDbContext.UserClaims).ToListAsync(cancellationToken);
        }
    }
}

using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Application.Common;
using Microsoft.EntityFrameworkCore;
using Application.Common.Exceptions;
using AutoMapper;
using Account.Contracts.Claims.Commands;
using Account.Contracts.Claims.Responses;

public class CreateUserClaimCommandHandler : IRequestHandler<CreateUserClaimCommand, SingleClaimResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IMapper _mapper;

    public CreateUserClaimCommandHandler(UserManager<ApplicationUser> userManager, IApplicationDbContext applicationDbContext, IMapper mapper)
    {
        _userManager = userManager;
        _applicationDbContext = applicationDbContext;
        _mapper = mapper;
    }

    public async Task<SingleClaimResponse> Handle(CreateUserClaimCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == request.UserId,
                 cancellationToken: cancellationToken);
        if (user == null)
            throw new ValidationFailedException("User not found");

        var newUserClaim = _mapper.Map<ApplicationUserClaim>(request);
        newUserClaim.Slug = $"{user.UserName}-{request.ClaimType}";
        await _applicationDbContext.UserClaims.AddAsync(newUserClaim, cancellationToken);
        await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<SingleClaimResponse>(newUserClaim);
    }
}
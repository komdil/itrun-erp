using Account.Contracts.UserRoles.Commands;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Account.Contracts.UserRoles.Responses;
using Application.Common;
using Microsoft.EntityFrameworkCore;
using Application.Common.Exceptions;
using AutoMapper;

public class CreateUserRoleCommandHandler : IRequestHandler<CreateUserRolesCommand, UserRolesResponse>
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IMapper _mapper;

    public CreateUserRoleCommandHandler(RoleManager<ApplicationRole> roleManager,
        UserManager<ApplicationUser> userManager, IApplicationDbContext applicationDbContext, IMapper mapper)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _applicationDbContext = applicationDbContext;
        _mapper = mapper;
    }

    public async Task<UserRolesResponse> Handle(CreateUserRolesCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == request.UserId,
                 cancellationToken: cancellationToken);
        if (user == null)
            throw new ValidationFailedException("User not found");

        var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Name == request.RoleName,
            cancellationToken: cancellationToken);
        if (role == null)
            throw new ValidationFailedException("Role not found");

        var newUserRole = _mapper.Map<ApplicationUserRole>(request);
        newUserRole.RoleId = role.Id;
        newUserRole.Slug = $"{user.UserName}-{role.Name}";
        await _applicationDbContext.UserRoles.AddAsync(newUserRole, cancellationToken);
        await _applicationDbContext.SaveChangesAsync(cancellationToken);

        var mapped = _mapper.Map<UserRolesResponse>(newUserRole);
        mapped.UserName = user.UserName;
        mapped.RoleName = role.Name;
        return mapped;
    }
}
using Application.Contract.ApplicationRoles.Responses;

using MediatR;

namespace Application.Contract.ApplicationRoles.Queries;
public class GetSingleRoleQuery : IRequest<RoleNameResponse>
{
    public string Name { get; set; }
}

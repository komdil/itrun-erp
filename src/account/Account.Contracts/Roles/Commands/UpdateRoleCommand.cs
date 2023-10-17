using Application.Contract.ApplicationRoles.Responses;
using MediatR;

namespace Application.Contract.ApplicationRoles.Commands;
public class UpdateRoleCommand : IRequest<RoleNameResponse>
{
    public string Name { get; set; }
    public string NewRoleName { get; set; }
}

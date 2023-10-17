using Application.Contract.ApplicationRoles.Responses;
using MediatR;

namespace Application.Contract.ApplicationRoles.Commands;

public class CreateRoleCommand : IRequest<RoleNameResponse>
{
    public string Name { get; set; }
}
using Application.Contract.ApplicationRoles.Responses;
using MediatR;

namespace Application.Contract.ApplicationRoles.Commands;
public class DeleteRoleCommand : IRequest
{
    public string Name { get; set; }
}

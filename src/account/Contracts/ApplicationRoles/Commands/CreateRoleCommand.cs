using Contracts;
using MediatR;

namespace Application.Contract.ApplicationRoles.Commands;

public class CreateRoleCommand:IRequest<ApplicationResponse<Guid>>
{
    public string RoleName { get; set; }
}
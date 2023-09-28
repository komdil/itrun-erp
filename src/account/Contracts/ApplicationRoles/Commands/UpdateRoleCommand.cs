using Contracts;
using MediatR;

namespace Application.Contract.ApplicationRoles.Commands;
public class UpdateRoleCommand : IRequest<ApplicationResponse<Guid>>
{
    public string Slug { get; set; }
    public string NewRoleName { get; set; }
}

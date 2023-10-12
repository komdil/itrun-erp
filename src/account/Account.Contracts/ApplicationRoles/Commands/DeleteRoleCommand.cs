using Contracts;
using MediatR;

namespace Application.Contract.ApplicationRoles.Commands;
public class DeleteRoleCommand : IRequest<ApplicationResponse<bool>>
{
    public string Slug { get; set; }
}

using Application.Contract.ApplicationRoles.Responses;
using Contracts;
using MediatR;

namespace Application.Contract.ApplicationRoles.Queries;
public class GetRoleBySlugQuery : IRequest<ApplicationResponse<RoleNameResponse>>
{
    public string Slug { get; set; }
}

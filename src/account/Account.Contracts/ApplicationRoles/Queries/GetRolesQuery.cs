using MediatR;
using Application.Contract.ApplicationRoles.Responses;
using Contracts;

namespace Application.Contract.ApplicationRoles.Queries;

public class GetRolesQuery : IRequest<ApplicationResponse<List<RoleNameResponse>>>
{
}
using MediatR;
using Application.Contract.ApplicationRoles.Responses;


namespace Application.Contract.ApplicationRoles.Queries;

public class GetRolesQuery : IRequest<List<RoleNameResponse>>
{
}
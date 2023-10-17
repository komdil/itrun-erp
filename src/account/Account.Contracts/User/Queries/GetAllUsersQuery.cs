using Account.Contracts.User.Response;
using MediatR;

namespace Account.Contracts.User.Queries
{
    public class GetUsersQuery : IRequest<List<UserResponse>>
    {
    }
}

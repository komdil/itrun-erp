using Contracts.User.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.User.Queries
{
    public class GetAllUsersQuery : IRequest<List<UserResponse>>
    {
    }
}

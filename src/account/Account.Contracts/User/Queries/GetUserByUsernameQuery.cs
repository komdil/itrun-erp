using Account.Contracts.User.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Contracts.User.Queries
{
    public class GetSingleUserQuery : IRequest<UserResponse>
    {
        public string UserName { get; set; }
    }
}

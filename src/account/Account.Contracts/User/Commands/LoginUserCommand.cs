using Contracts.User.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.User.Commands
{
    public class LoginUserCommand : IRequest<ApplicationResponse<JwtTokenResponse>>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}

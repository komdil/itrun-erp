using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.User.Commands
{
    public class RegisterUserCommand : IRequest<ApplicationResponse<Guid>>
    {
        public string Email { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string PhoneNumber { get; set; } = "";

        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
    }
}

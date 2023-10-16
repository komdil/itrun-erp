using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.User.Commands
{
    public class ChangePasswordUserCommand : IRequest<Guid>
    {
    }
}

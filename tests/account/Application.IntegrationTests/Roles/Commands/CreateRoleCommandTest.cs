using Application.Contract.ApplicationRoles.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Roles.Commands
{
    public class CreateRoleCommandTest
    {
        [Test]
        public async Task CreateRoleCommand_ShouldCreateRoleCommand_Success()
        {
            var command = new CreateRoleCommand
            {
                RoleName = "Test",
            };

        }

    }
}

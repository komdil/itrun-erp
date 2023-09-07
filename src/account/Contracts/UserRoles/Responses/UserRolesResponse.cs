using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.UserRoles.Responses
{
    public class UserRolesResponse
    {
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public string Slug { get; set; }
    }
}

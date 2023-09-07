using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ApplicationRoles.Responses
{
    public class RoleNameResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; } = "";
        public string Slug { get; set; }
    }
}

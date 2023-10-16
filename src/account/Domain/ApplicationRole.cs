using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public string Slug { get; set; }
        public List<ApplicationUserRole> UserRoles { get; set; }
    }
}

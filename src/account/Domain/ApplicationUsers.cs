using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ApplicationUsers : IdentityUser<Guid>
    {
        public List<ApplicationUserRole> UserRoles { get; set; }
    }
}

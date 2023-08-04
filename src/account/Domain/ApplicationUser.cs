using Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
	public class ApplicationUser : IdentityUser<Guid>
	{
		public Organization Organization { get; set; }

		public Guid? PrimaryContactId { get; set; }

		public Contact PrimaryContact { get; set; }
	}
}

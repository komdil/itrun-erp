using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
	public class Contact
	{
		public Guid Id { get; set; }
		public string Address { get; set; }

        public ICollection<ApplicationUser> Users { get; set; }
    }
}

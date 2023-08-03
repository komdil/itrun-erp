﻿using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
	public class Organization
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }
		public string Info { get; set; }
		public Guid OwnerId { get; set; }
		public ApplicationUser Owner { get; set; }
	}
}

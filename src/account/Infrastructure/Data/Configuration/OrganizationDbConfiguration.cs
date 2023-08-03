using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Configuration
{
	internal class OrganizationDbConfiguration : IEntityTypeConfiguration<Organization>
	{
		public void Configure(EntityTypeBuilder<Organization> builder)
		{
			builder.HasKey(o => o.Id);
			builder.HasOne(o => o.Owner).WithOne().HasForeignKey<Organization>(s=>s.OwnerId);
			
		}
	}
}

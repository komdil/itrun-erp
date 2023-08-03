using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Configuration
{
	internal class ApplicationUserDbConfiguration : IEntityTypeConfiguration<ApplicationUserDbConfiguration>
	{
		public void Configure(EntityTypeBuilder<ApplicationUserDbConfiguration> builder)
		{
			throw new NotImplementedException();
		}
	}
}

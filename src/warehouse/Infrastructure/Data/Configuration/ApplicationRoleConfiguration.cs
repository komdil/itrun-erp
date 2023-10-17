using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Warehouse.Infrastructure.Data.Configuration;
public class ApplicationRoleConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasOne(s => s.ParentCategory).WithMany().HasForeignKey(s => s.ParentCategoryId);
    }
}

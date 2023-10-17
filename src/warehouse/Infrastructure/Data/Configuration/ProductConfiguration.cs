using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Warehouse.Infrastructure.Data.Configuration;
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasOne(s => s.Category).WithMany().HasForeignKey(s => s.CategoryId);
        builder.HasOne(s => s.Warehouse).WithMany().HasForeignKey(s => s.WarehouseId);
    }
}

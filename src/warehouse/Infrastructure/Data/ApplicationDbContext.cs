using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<WareHouse> Warehouses { get; set; }

        public DbSet<ProductUOM> ProductUOMs  { get; set; }

		public DbSet<Product> Products { get; set; }
       
        public DbSet<SaleProduct> SaleProducts {  get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<ProductPurchase> ProductPurchases { get; set; }
    }
}
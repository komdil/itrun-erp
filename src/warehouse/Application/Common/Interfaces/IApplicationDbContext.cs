﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<WareHouse> Warehouses { get; }
        DbSet<ProductUOM> ProductUOMs { get; }
        DbSet<Product> Products { get; }
        DbSet<Category> Categories { get; }
        DbSet<SaleProduct> SaleProducts { get; }
        DbSet<ProductPurchase> ProductPurchases { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

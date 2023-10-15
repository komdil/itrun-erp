﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<WareHouse> Warehouses { get; }

        DbSet<ProductUOM> ProductUOMs { get; }

        DbSet<Product> Products { get; }
       

        DbSet<SaleProduct> SaleProducts { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

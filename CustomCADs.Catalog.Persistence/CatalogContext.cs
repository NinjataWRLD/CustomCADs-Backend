﻿using CustomCADs.Catalog.Domain.Categories;
using CustomCADs.Catalog.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Catalog.Persistence;

public class CatalogContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(CatalogPersistenceAssemblyReference.Assembly);
    }
}

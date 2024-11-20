using CustomCADs.Catalog.Domain.Categories;
using CustomCADs.Catalog.Domain.Products;

namespace CustomCADs.Catalog.Persistence;

public class CatalogContext(DbContextOptions<CatalogContext> opts) : DbContext(opts)
{
    public required DbSet<Category> Categories { get; set; }
    public required DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("Catalog");
        builder.ApplyConfigurationsFromAssembly(CatalogPersistenceReference.Assembly);
    }
}

using CustomCADs.Catalog.Domain.Products;

namespace CustomCADs.Catalog.Persistence;

public class InventoryContext(DbContextOptions<InventoryContext> opts) : DbContext(opts)
{
    public required DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("Inventory");
        builder.ApplyConfigurationsFromAssembly(InventoryPersistenceReference.Assembly);
    }
}

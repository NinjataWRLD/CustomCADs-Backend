using CustomCADs.Inventory.Domain.Products;

namespace CustomCADs.Inventory.Persistence;

public class InventoryContext(DbContextOptions<InventoryContext> opts) : DbContext(opts)
{
    public required DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("Inventory");
        builder.ApplyConfigurationsFromAssembly(InventoryPersistenceReference.Assembly);
    }
}

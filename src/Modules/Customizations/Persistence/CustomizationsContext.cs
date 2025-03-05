using CustomCADs.Customizations.Domain.Customizations;
using CustomCADs.Customizations.Domain.Materials;

namespace CustomCADs.Customizations.Persistence;

public class CustomizationsContext(DbContextOptions<CustomizationsContext> opts) : DbContext(opts)
{
    public required DbSet<Customization> Customizations { get; set; }
    public required DbSet<Material> Materials { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("Customizations");
        builder.ApplyConfigurationsFromAssembly(CustomizationPersistenceReference.Assembly);
    }
}

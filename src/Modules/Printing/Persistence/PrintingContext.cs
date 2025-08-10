using CustomCADs.Printing.Domain.Customizations;
using CustomCADs.Printing.Domain.Materials;

namespace CustomCADs.Printing.Persistence;

public class PrintingContext(DbContextOptions<PrintingContext> opts) : DbContext(opts)
{
	public required DbSet<Customization> Customizations { get; set; }
	public required DbSet<Material> Materials { get; set; }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.HasDefaultSchema("Printing");
		builder.ApplyConfigurationsFromAssembly(PrintingPersistenceReference.Assembly);
	}
}

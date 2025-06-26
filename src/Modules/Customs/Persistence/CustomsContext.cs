using CustomCADs.Customs.Domain.Customs;
using CustomCADs.Customs.Domain.Customs.States.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Customs.Persistence;

public class CustomsContext(DbContextOptions<CustomsContext> opts) : DbContext(opts)
{
	public required DbSet<Custom> Customs { get; set; }
	public required DbSet<AcceptedCustom> AcceptedCustoms { get; set; }
	public required DbSet<FinishedCustom> FinishedCustoms { get; set; }
	public required DbSet<CompletedCustom> CompletedCustoms { get; set; }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.HasDefaultSchema("Customs");
		builder.ApplyConfigurationsFromAssembly(CustomsPersistenceReference.Assembly);
	}
}

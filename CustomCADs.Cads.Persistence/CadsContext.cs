using CustomCADs.Cads.Domain.Cads.Entites;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Cads.Persistence;

public class CadsContext(DbContextOptions<CadsContext> opts) : DbContext(opts)
{
    public required DbSet<Cad> Cads { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("Cads");
        builder.ApplyConfigurationsFromAssembly(CadsPersistenceReference.Assembly);
    }
}

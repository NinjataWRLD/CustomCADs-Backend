using CustomCADs.Files.Domain.Cads;
using CustomCADs.Files.Domain.Images;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Files.Persistence;

public class FilesContext(DbContextOptions<FilesContext> opts) : DbContext(opts)
{
    public required DbSet<Cad> Cads { get; set; }
    public required DbSet<Image> Images { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("Files");
        builder.ApplyConfigurationsFromAssembly(FilesPersistenceReference.Assembly);
    }
}

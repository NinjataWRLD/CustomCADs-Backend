using CustomCADs.Categories.Domain.Categories;

namespace CustomCADs.Categories.Persistence;

public class CategoriesContext(DbContextOptions<CategoriesContext> opts) : DbContext(opts)
{
    public required DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("Categories");
        builder.ApplyConfigurationsFromAssembly(CategoriesPersistenceReference.Assembly);
    }
}

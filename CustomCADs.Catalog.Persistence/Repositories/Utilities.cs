using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Catalog.Persistence.Repositories;

public static class Utilities
{
    public static IQueryable<TEntity> WithTracking<TEntity>(
        this DbSet<TEntity> entities,
        bool track) where TEntity : class
        => track
            ? entities
            : entities.AsNoTracking();
}

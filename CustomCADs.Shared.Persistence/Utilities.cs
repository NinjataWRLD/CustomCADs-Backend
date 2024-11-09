using CustomCADs.Shared.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Shared.Persistence;

public static class Utilities
{
    public static IQueryable<TEntity> WithTracking<TEntity>(
        this DbSet<TEntity> entities,
        bool track) where TEntity : class, IEntity
        => track
            ? entities
            : entities.AsNoTracking();
}

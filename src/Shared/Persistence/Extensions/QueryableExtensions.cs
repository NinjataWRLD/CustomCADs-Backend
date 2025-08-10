using CustomCADs.Shared.Domain.Bases.Entities;
using CustomCADs.Shared.Domain.Querying;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Shared.Persistence.Extensions;

public static class QueryableExtensions
{
	public static IQueryable<TEntity> WithTracking<TEntity>(
		this DbSet<TEntity> set,
		bool track
	) where TEntity : BaseAggregateRoot
		=> track ? set : set.AsNoTracking();

	public static IQueryable<TEntity> WithPagination<TEntity>(
		this IQueryable<TEntity> query,
		Pagination pagination
	) => query
			.Skip((pagination.Page - 1) * pagination.Limit)
			.Take(pagination.Limit);
}

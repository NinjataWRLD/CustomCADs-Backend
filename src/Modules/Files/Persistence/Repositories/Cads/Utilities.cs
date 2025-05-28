using CustomCADs.Files.Domain.Cads;
using CustomCADs.Files.Domain.Cads.Enums;
using CustomCADs.Files.Domain.Cads.ValueObjects;
using CustomCADs.Shared.Core.Common.Enums;
using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.Files.Persistence.Repositories.Cads;

public static class Utilities
{
	public static IQueryable<Cad> WithFilter(this IQueryable<Cad> query, CadId[]? ids)
	{
		if (ids is not null)
		{
			query = query.Where(c => ids.Contains(c.Id));
		}

		return query;
	}

	public static IQueryable<Cad> WithSorting(this IQueryable<Cad> query, CadSorting? sorting = null)
	{
		return sorting switch
		{
			{ Type: CadSortingType.CreationDate, Direction: SortingDirection.Ascending } => query.OrderBy(c => c.Id), // will fix
			{ Type: CadSortingType.CreationDate, Direction: SortingDirection.Descending } => query.OrderByDescending(c => c.Id), // will fix
			_ => query,
		};
	}

	public static IQueryable<Cad> WithPagination(this IQueryable<Cad> query, int page = 1, int limit = 20)
	{
		return query.Skip((page - 1) * limit).Take(limit);
	}
}

using CustomCADs.Files.Domain.Cads;
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

	public static IQueryable<Cad> WithPagination(this IQueryable<Cad> query, int page = 1, int limit = 20)
	{
		return query.Skip((page - 1) * limit).Take(limit);
	}
}

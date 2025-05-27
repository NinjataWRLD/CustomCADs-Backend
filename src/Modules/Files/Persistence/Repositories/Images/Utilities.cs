using CustomCADs.Files.Domain.Images;
using CustomCADs.Files.Domain.Images.Enums;
using CustomCADs.Files.Domain.Images.ValueObjects;
using CustomCADs.Shared.Core.Common.Enums;
using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.Files.Persistence.Repositories.Images;

public static class Utilities
{
	public static IQueryable<Image> WithFilter(this IQueryable<Image> query, ImageId[]? ids)
	{
		if (ids is not null)
		{
			query = query.Where(c => ids.Contains(c.Id));
		}

		return query;
	}

	public static IQueryable<Image> WithSorting(this IQueryable<Image> query, ImageSorting? sorting = null)
	{
		return sorting switch
		{
			{ Type: ImageSortingType.CreationDate, Direction: SortingDirection.Ascending } => query.OrderBy(c => c.Id), // will fix
			{ Type: ImageSortingType.CreationDate, Direction: SortingDirection.Descending } => query.OrderByDescending(c => c.Id), // will fix
			_ => query,
		};
	}

	public static IQueryable<Image> WithPagination(this IQueryable<Image> query, int page = 1, int limit = 20)
	{
		return query.Skip((page - 1) * limit).Take(limit);
	}
}

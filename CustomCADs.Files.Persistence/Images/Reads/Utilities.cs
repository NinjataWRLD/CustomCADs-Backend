using CustomCADs.Files.Domain.Images;
using CustomCADs.Files.Domain.Images.Enums;
using CustomCADs.Files.Domain.Images.ValueObjects;
using CustomCADs.Shared.Core.Common.Enums;

namespace CustomCADs.Files.Persistence.Images.Reads;

public static class Utilities
{
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

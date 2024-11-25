using CustomCADs.Cads.Domain.Cads;
using CustomCADs.Cads.Domain.Cads.Enums;
using CustomCADs.Cads.Domain.Cads.ValueObjects;
using CustomCADs.Shared.Core.Common.Enums;

namespace CustomCADs.Cads.Persistence.Cads.Reads;

public static class Utilities
{
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

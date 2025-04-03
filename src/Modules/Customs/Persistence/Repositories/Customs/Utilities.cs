using CustomCADs.Customs.Domain.Customs;
using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Customs.Domain.Customs.ValueObjects;
using CustomCADs.Shared.Core.Common.Enums;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Customs.Persistence.Repositories.Customs;

public static class Utilities
{
    public static IQueryable<Custom> WithFilter(this IQueryable<Custom> query, bool? forDelivery = null, CustomStatus? status = null, AccountId? buyerId = null, AccountId? designerId = null)
    {
        if (forDelivery is not null)
        {
            query = query.Where(c => c.ForDelivery == forDelivery);
        }
        if (status is not null)
        {
            query = query.Where(c => c.CustomStatus == status);
        }
        if (buyerId is not null)
        {
            query = query.Where(c => c.BuyerId == buyerId);
        }
        if (designerId is not null)
        {
            query = query.Where(c => c.AcceptedCustom != null && c.AcceptedCustom.DesignerId == designerId);
        }

        return query;
    }

    public static IQueryable<Custom> WithSearch(this IQueryable<Custom> query, string? name = null)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            query = query.Where(c => c.Name.Contains(name));
        }

        return query;
    }

    public static IQueryable<Custom> WithSorting(this IQueryable<Custom> query, CustomSorting? sorting = null)
    {
        return sorting switch
        {
            { Type: CustomSortingType.OrderedAt, Direction: SortingDirection.Ascending } => query.OrderBy(c => c.OrderedAt),
            { Type: CustomSortingType.OrderedAt, Direction: SortingDirection.Descending } => query.OrderByDescending(c => c.OrderedAt),
            { Type: CustomSortingType.Alphabetical, Direction: SortingDirection.Ascending } => query.OrderBy(c => c.Name),
            { Type: CustomSortingType.Alphabetical, Direction: SortingDirection.Descending } => query.OrderByDescending(c => c.Name),
            { Type: CustomSortingType.CustomStatus, Direction: SortingDirection.Ascending } => query.OrderBy(m => (int)m.CustomStatus),
            { Type: CustomSortingType.CustomStatus, Direction: SortingDirection.Descending } => query.OrderByDescending(m => (int)m.CustomStatus),
            _ => query,
        };
    }

    public static IQueryable<Custom> WithPagination(this IQueryable<Custom> query, int page = 1, int limit = 20)
    {
        return query.Skip((page - 1) * limit).Take(limit);
    }
}

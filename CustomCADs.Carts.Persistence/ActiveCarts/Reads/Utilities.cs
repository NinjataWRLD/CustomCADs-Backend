using CustomCADs.Carts.Domain.ActiveCarts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Carts.Persistence.ActiveCarts.Reads;

public static class Utilities
{
    public static IQueryable<ActiveCart> WithFilter(this IQueryable<ActiveCart> query, ProductId? productId = null)
    {
        if (productId is not null)
        {
            query = query.Where(c => c.Items.Any(i => i.ProductId == productId));
        }

        return query;
    }

    public static IQueryable<ActiveCart> WithPagination(this IQueryable<ActiveCart> query, int page = 1, int limit = 20)
    {
        return query.Skip((page - 1) * limit).Take(limit);
    }
}

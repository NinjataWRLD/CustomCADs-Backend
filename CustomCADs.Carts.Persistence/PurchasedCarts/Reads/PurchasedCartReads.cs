using CustomCADs.Carts.Domain.PurchasedCarts;
using CustomCADs.Carts.Domain.PurchasedCarts.Reads;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Carts.Persistence.PurchasedCarts.Reads;

public sealed class PurchasedCartReads(CartsContext context) : IPurchasedCartReads
{
    public async Task<Result<PurchasedCart>> AllAsync(PurchasedCartQuery query, bool track = true, CancellationToken ct = default)
    {
        IQueryable<PurchasedCart> queryable = context.PurchasedCarts
            .WithTracking(track)
            .Include(c => c.Items)
            .WithFilter(query.BuyerId)
            .WithSorting(query.Sorting ?? new());

        int count = await queryable.CountAsync(ct).ConfigureAwait(false);
        PurchasedCart[] carts = await queryable
            .WithPagination(query.Pagination.Page, query.Pagination.Limit)
            .ToArrayAsync(ct)
            .ConfigureAwait(false);

        return new(count, carts);
    }

    public async Task<PurchasedCart?> SingleByIdAsync(PurchasedCartId id, bool track = true, CancellationToken ct = default)
        => await context.PurchasedCarts
            .WithTracking(track)
            .Include(c => c.Items)
            .FirstOrDefaultAsync(p => p.Id == id, ct)
            .ConfigureAwait(false);

    public async Task<int> CountAsync(AccountId buyerId, CancellationToken ct = default)
        => await context.PurchasedCarts
            .WithTracking(false)
            .Where(c => c.BuyerId == buyerId)
            .CountAsync(ct)
            .ConfigureAwait(false);

    public async Task<Dictionary<PurchasedCartId, int>> CountItemsAsync(AccountId buyerId, CancellationToken ct = default)
        => await context.PurchasedCarts
            .WithTracking(false)
            .Include(c => c.Items)
            .Where(c => c.BuyerId == buyerId)
            .ToDictionaryAsync(kv => kv.Id, kv => kv.Items.Count, ct)
            .ConfigureAwait(false);
}

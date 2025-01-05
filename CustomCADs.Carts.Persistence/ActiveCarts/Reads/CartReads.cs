using CustomCADs.Carts.Domain.ActiveCarts;
using CustomCADs.Carts.Domain.ActiveCarts.Reads;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Carts.Persistence.ActiveCarts.Reads;

public sealed class ActiveCartReads(CartsContext context) : IActiveCartReads
{
    public async Task<Result<ActiveCart>> AllAsync(ActiveCartQuery query, bool track = true, CancellationToken ct = default)
    {
        IQueryable<ActiveCart> queryable = context.ActiveCarts
            .WithTracking(track)
            .Include(c => c.Items)
            .WithFilter(query.ProductId);

        int count = await queryable.CountAsync(ct).ConfigureAwait(false);
        ActiveCart[] carts = await queryable
            .WithPagination(query.Pagination.Page, query.Pagination.Limit)
            .ToArrayAsync(ct)
            .ConfigureAwait(false);

        return new(count, carts);
    }

    public async Task<ActiveCart?> SingleByIdAsync(ActiveCartId id, bool track = true, CancellationToken ct = default)
        => await context.ActiveCarts
            .WithTracking(track)
            .Include(c => c.Items)
            .SingleOrDefaultAsync(p => p.Id == id, ct)
            .ConfigureAwait(false);

    public async Task<ActiveCart?> SingleByBuyerIdAsync(AccountId buyerId, bool track = true, CancellationToken ct = default)
        => await context.ActiveCarts
            .WithTracking(track)
            .Include(c => c.Items)
            .SingleOrDefaultAsync(p => p.BuyerId == buyerId, ct)
            .ConfigureAwait(false);

    public async Task<bool> ExistsByIdAsync(ActiveCartId id, CancellationToken ct = default)
        => await context.ActiveCarts
            .WithTracking(false)
            .AnyAsync(c => c.Id == id, ct)
            .ConfigureAwait(false);

    public async Task<int> CountByProductIdAsync(ProductId productId, CancellationToken ct = default)
        => await context.ActiveCarts
            .WithTracking(false)
            .Where(c => c.Items.Any(i => i.ProductId == productId))
            .CountAsync(ct)
            .ConfigureAwait(false);

    public async Task<int> CountItemsByBuyerIdAsync(AccountId buyerId, CancellationToken ct = default)
        => await context.ActiveCarts
            .WithTracking(false)
            .Include(c => c.Items)
            .Where(c => c.BuyerId == buyerId)
            .SelectMany(c => c.Items)
            .CountAsync(ct)
            .ConfigureAwait(false);
}

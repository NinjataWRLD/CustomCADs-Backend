using CustomCADs.Gallery.Domain.Carts;
using CustomCADs.Gallery.Domain.Carts.Reads;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Account;
using CustomCADs.Shared.Core.Common.TypedIds.Gallery;
using CustomCADs.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Gallery.Persistence.Carts.Reads;

public class CartReads(GalleryContext context) : ICartReads
{
    public async Task<Result<Cart>> AllAsync(CartQuery query, bool track = true, CancellationToken ct = default)
    {
        IQueryable<Cart> queryable = context.Carts
            .WithTracking(track)
            .Include(c => c.Items)
            .WithFilter(query.BuyerId)
            .WithSorting(query.Sorting ?? new());

        int count = await queryable.CountAsync(ct).ConfigureAwait(false);
        Cart[] carts = await queryable
            .WithPagination(query.Page, query.Limit)
            .ToArrayAsync(ct)
            .ConfigureAwait(false);

        return new(count, carts);
    }

    public async Task<Cart?> SingleByIdAsync(CartId id, bool track = true, CancellationToken ct = default)
        => await context.Carts
            .WithTracking(track)
            .Include(c => c.Items)
            .FirstOrDefaultAsync(p => p.Id == id, ct)
            .ConfigureAwait(false);

    public async Task<bool> ExistsByIdAsync(CartId id, CancellationToken ct = default)
        => await context.Carts
            .WithTracking(false)
            .AnyAsync(c => c.Id == id, ct)
            .ConfigureAwait(false);

    public async Task<int> CountAsync(AccountId buyerId, CancellationToken ct = default)
        => await context.Carts
            .WithTracking(false)
            .Where(c => c.BuyerId == buyerId)
            .CountAsync(ct)
            .ConfigureAwait(false);

    public async Task<Dictionary<CartId, int>> CountItemsAsync(AccountId buyerId, CancellationToken ct = default)
        => await context.Carts
            .WithTracking(false)
            .Include(c => c.Items)
            .Where(c => c.BuyerId == buyerId)
            .ToDictionaryAsync(kv => kv.Id, kv => kv.Items.Count, ct)
            .ConfigureAwait(false);
}

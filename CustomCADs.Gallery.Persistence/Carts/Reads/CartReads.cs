using CustomCADs.Gallery.Domain.Carts.Entities;
using CustomCADs.Gallery.Domain.Carts.Reads;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Gallery;
using CustomCADs.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Gallery.Persistence.Carts.Reads;

public class CartReads(GalleryContext context) : ICartReads
{
    public async Task<CartResult> AllAsync(CartQuery query, bool track = true, CancellationToken ct = default)
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

    public async Task<ICollection<CartItem>> ItemsByIdAsync(CartId id, CancellationToken ct = default)
        => await context.Carts
            .WithTracking(false)
            .Include(c => c.Items)
            .Where(o => o.Id == id)
            .SelectMany(c => c.Items)
            .ToArrayAsync(ct)
            .ConfigureAwait(false);

    public async Task<bool> ExistsByIdAsync(CartId id, CancellationToken ct = default)
        => await context.Carts
            .WithTracking(false)
            .AnyAsync(o => o.Id == id, ct)
            .ConfigureAwait(false);

    public async Task<int> CountAsync(UserId buyerId, CancellationToken ct = default)
        => await context.Carts
            .WithTracking(false)
            .Where(o => o.BuyerId == buyerId)
            .CountAsync(ct)
            .ConfigureAwait(false);
}

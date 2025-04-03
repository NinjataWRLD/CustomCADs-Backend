using CustomCADs.Carts.Domain.ActiveCarts;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Carts.Persistence.Repositories.ActiveCarts;

public sealed class Reads(CartsContext context) : IActiveCartReads
{
    public async Task<ActiveCartItem[]> AllAsync(AccountId buyerId, bool track = true, CancellationToken ct = default)
        => await context.ActiveCartItems
            .WithTracking(track)
            .Where(p => p.BuyerId == buyerId)
            .OrderByDescending(p => p.AddedAt)
            .ToArrayAsync(ct)
            .ConfigureAwait(false);

    public async Task<ActiveCartItem[]> AllAsync(ProductId productId, bool track = true, CancellationToken ct = default)
        => await context.ActiveCartItems
            .WithTracking(track)
            .Where(p => p.ProductId == productId)
            .ToArrayAsync(ct)
            .ConfigureAwait(false);

    public async Task<ActiveCartItem?> SingleAsync(AccountId buyerId, ProductId productId, bool track = true, CancellationToken ct = default)
        => await context.ActiveCartItems
            .WithTracking(track)
            .FirstOrDefaultAsync(c => c.BuyerId == buyerId && c.ProductId == productId, ct)
            .ConfigureAwait(false);

    public async Task<bool> ExistsAsync(AccountId buyerId, CancellationToken ct = default)
        => await context.ActiveCartItems
            .WithTracking(false)
            .AnyAsync(c => c.BuyerId == buyerId, ct)
            .ConfigureAwait(false);

    public async Task<int> CountAsync(AccountId buyerId, CancellationToken ct = default)
        => await context.ActiveCartItems
            .WithTracking(false)
            .Where(c => c.BuyerId == buyerId)
            .CountAsync(ct)
            .ConfigureAwait(false);
}

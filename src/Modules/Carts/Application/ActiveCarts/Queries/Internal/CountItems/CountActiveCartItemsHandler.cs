﻿using CustomCADs.Carts.Domain.Repositories.Reads;

namespace CustomCADs.Carts.Application.ActiveCarts.Queries.Internal.CountItems;

public sealed class CountActiveCartItemsHandler(IActiveCartReads reads)
    : IQueryHandler<CountActiveCartItemsQuery, int>
{
    public async Task<int> Handle(CountActiveCartItemsQuery req, CancellationToken ct)
    {
        return await reads.CountItemsByBuyerIdAsync(req.BuyerId, ct: ct).ConfigureAwait(false);
    }
}

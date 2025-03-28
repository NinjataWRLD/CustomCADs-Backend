﻿using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Carts.Application.ActiveCarts.Queries.Internal.GetByBuyerId;

public sealed class GetActiveCartHandler(IActiveCartReads reads, IRequestSender sender)
    : IQueryHandler<GetActiveCartQuery, GetActiveCartDto>
{
    public async Task<GetActiveCartDto> Handle(GetActiveCartQuery req, CancellationToken ct)
    {
        ActiveCart cart = await reads.SingleByBuyerIdAsync(req.BuyerId, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<ActiveCart>.ById(req.BuyerId);

        GetUsernameByIdQuery buyerQuery = new(cart.BuyerId);
        string buyer = await sender.SendQueryAsync(buyerQuery, ct).ConfigureAwait(false);

        return cart.ToDto(buyer);
    }
}

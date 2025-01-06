﻿using CustomCADs.Carts.Application.Common.Exceptions;
using CustomCADs.Carts.Domain.ActiveCarts.Entities;
using CustomCADs.Carts.Domain.ActiveCarts.Reads;
using CustomCADs.Carts.Domain.Common;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.IncrementQuantity;

public class IncreaseActiveCartItemQuantityHandler(IActiveCartReads reads, IUnitOfWork uow)
    : ICommandHandler<IncreaseActiveCartItemQuantityCommand, int>
{
    public async Task<int> Handle(IncreaseActiveCartItemQuantityCommand req, CancellationToken ct)
    {
        ActiveCart cart = await reads.SingleByBuyerIdAsync(req.BuyerId, ct: ct).ConfigureAwait(false)
            ?? throw ActiveCartNotFoundException.ByBuyerId(req.BuyerId);

        ActiveCartItem item = cart.Items.SingleOrDefault(i => i.Id == req.ItemId)
            ?? throw ActiveCartItemNotFoundException.ById(req.ItemId);

        item.IncreaseQuantity(req.Amount);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        return item.Quantity;
    }
}

﻿using CustomCADs.Carts.Domain.ActiveCarts.Entities;
using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Carts.Domain.Repositories.Reads;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Item.Remove;

public sealed class RemoveActiveCartItemHandler(IActiveCartReads reads, IUnitOfWork uow)
    : ICommandHandler<RemoveActiveCartItemCommand>
{
    public async Task Handle(RemoveActiveCartItemCommand req, CancellationToken ct)
    {
        ActiveCart cart = await reads.SingleByBuyerIdAsync(req.BuyerId, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<ActiveCart>.ById(req.BuyerId);

        ActiveCartItem item = cart.Items.FirstOrDefault(i => i.ProductId == req.ProductId)
            ?? throw CustomNotFoundException<ActiveCartItem>.ById(req.ProductId);

        cart.RemoveItem(item);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}

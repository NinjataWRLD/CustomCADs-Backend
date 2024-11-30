﻿using CustomCADs.Gallery.Application.Carts.Exceptions;
using CustomCADs.Gallery.Domain.Carts;
using CustomCADs.Gallery.Domain.Carts.Reads;
using CustomCADs.Gallery.Domain.Common;

namespace CustomCADs.Gallery.Application.Carts.Commands.RemoveItem;

public class RemoveCartItemHandler(ICartReads reads, IUnitOfWork uow)
    : ICommandHandler<RemoveCartItemCommand>
{
    public async Task Handle(RemoveCartItemCommand req, CancellationToken ct)
    {
        Cart cart = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw CartNotFoundException.ById(req.Id);

        if (cart.BuyerId == req.BuyerId)
        {
            throw CartAuthorizationException.ByCartId(req.Id);
        }

        cart.RemoveItem(req.ItemId);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}

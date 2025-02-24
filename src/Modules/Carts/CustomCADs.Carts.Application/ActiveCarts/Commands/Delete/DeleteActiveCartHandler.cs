﻿using CustomCADs.Carts.Application.Common.Exceptions;
using CustomCADs.Carts.Domain.ActiveCarts.Reads;
using CustomCADs.Carts.Domain.Common;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.Delete;

public sealed class DeleteActiveCartHandler(IActiveCartReads reads, IWrites<ActiveCart> writes, IUnitOfWork uow)
    : ICommandHandler<DeleteActiveCartCommand>
{
    public async Task Handle(DeleteActiveCartCommand req, CancellationToken ct)
    {
        ActiveCart cart = await reads.SingleByBuyerIdAsync(req.BuyerId, ct: ct).ConfigureAwait(false)
            ?? throw ActiveCartNotFoundException.ByBuyerId(req.BuyerId);

        writes.Remove(cart);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}

using CustomCADs.Orders.Application.Common.Exceptions;
using CustomCADs.Orders.Domain.Common;
using CustomCADs.Orders.Domain.Orders.Reads;

namespace CustomCADs.Orders.Application.Orders.Commands.Cancel;

public sealed class CancelOrderHandler(IOrderReads reads, IUnitOfWork uow)
    : ICommandHandler<CancelOrderCommand>
{
    public async Task Handle(CancelOrderCommand req, CancellationToken ct)
    {
        Order order = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw OrderNotFoundException.ById(req.Id);

        if (req.DesignerId != order.DesignerId)
        {
            throw OrderAuthorizationException.NotAssociated("cancel");
        }

        order.SetPendingStatus();
        order.EraseDesignerId();

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}


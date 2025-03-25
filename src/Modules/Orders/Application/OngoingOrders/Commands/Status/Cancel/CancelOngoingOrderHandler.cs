using CustomCADs.Orders.Domain.Repositories;
using CustomCADs.Orders.Domain.Repositories.Reads;

namespace CustomCADs.Orders.Application.OngoingOrders.Commands.Status.Cancel;

public sealed class CancelOngoingOrderHandler(IOngoingOrderReads reads, IUnitOfWork uow)
    : ICommandHandler<CancelOngoingOrderCommand>
{
    public async Task Handle(CancelOngoingOrderCommand req, CancellationToken ct)
    {
        OngoingOrder order = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<OngoingOrder>.ById(req.Id);

        if (req.DesignerId != order.DesignerId)
            throw CustomAuthorizationException<OngoingOrder>.ById(req.Id);

        order.SetPendingStatus();
        order.EraseDesignerId();

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}


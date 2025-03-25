using CustomCADs.Orders.Domain.Repositories;
using CustomCADs.Orders.Domain.Repositories.Reads;

namespace CustomCADs.Orders.Application.OngoingOrders.Commands.Status.Begin;

public sealed class BeginOngoingOrderHandler(IOngoingOrderReads reads, IUnitOfWork uow)
    : ICommandHandler<BeginOngoingOrderCommand>
{
    public async Task Handle(BeginOngoingOrderCommand req, CancellationToken ct)
    {
        OngoingOrder order = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<OngoingOrder>.ById(req.Id);

        if (req.DesignerId != order.DesignerId)
            throw CustomAuthorizationException<OngoingOrder>.ById(req.Id);

        order.SetBegunStatus();
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}


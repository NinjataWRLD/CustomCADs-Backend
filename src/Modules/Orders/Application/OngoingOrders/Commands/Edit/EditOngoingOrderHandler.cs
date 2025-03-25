using CustomCADs.Orders.Domain.Repositories;
using CustomCADs.Orders.Domain.Repositories.Reads;

namespace CustomCADs.Orders.Application.OngoingOrders.Commands.Edit;

public sealed class EditOngoingOrderHandler(IOngoingOrderReads reads, IUnitOfWork uow)
    : ICommandHandler<EditOngoingOrderCommand>
{
    public async Task Handle(EditOngoingOrderCommand req, CancellationToken ct)
    {
        OngoingOrder order = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<OngoingOrder>.ById(req.Id);

        if (order.BuyerId != req.BuyerId)
            throw CustomAuthorizationException<OngoingOrder>.ById(req.Id);

        order
            .SetName(req.Name)
            .SetDescription(req.Description);

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}

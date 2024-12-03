using CustomCADs.Orders.Application.Common.Exceptions;
using CustomCADs.Orders.Domain.Common;
using CustomCADs.Orders.Domain.Orders;
using CustomCADs.Orders.Domain.Orders.Reads;

namespace CustomCADs.Orders.Application.Orders.Commands.Complete;

public sealed class CompleteOrderHandler(IOrderReads reads, IUnitOfWork uow)
    : ICommandHandler<CompleteOrderCommand>
{
    public async Task Handle(CompleteOrderCommand req, CancellationToken ct)
    {
        Order order = await reads.SingleByIdAsync(req.Id, ct: ct)
            ?? throw OrderNotFoundException.ById(req.Id);

        if (req.BuyerId != order.BuyerId)
        {
            throw OrderAuthorizationException.ByOrderId(req.Id);
        }
        order.SetCompletedStatus();

        await uow.SaveChangesAsync(ct);
    }
}

using CustomCADs.Orders.Domain.Common;
using CustomCADs.Orders.Domain.OngoingOrders.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.Shared.UseCases.Cads.Commands;

namespace CustomCADs.Orders.Application.OngoingOrders.Commands.Status.Finish;

public sealed class FinishOngoingOrderHandler(IOngoingOrderReads reads, IUnitOfWork uow, IRequestSender sender)
    : ICommandHandler<FinishOngoingOrderCommand>
{
    public async Task Handle(FinishOngoingOrderCommand req, CancellationToken ct)
    {
        OngoingOrder order = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw OngoingOrderNotFoundException.ById(req.Id);

        if (req.DesignerId != order.DesignerId)
        {
            throw OngoingOrderAuthorizationException.NotAssociated(order.Id, "finish");
        }
        order.SetFinishedStatus();

        CreateCadCommand cadCommand = new(
            Key: req.Cad.Key,
            ContentType: req.Cad.ContentType
        );
        CadId cadId = await sender.SendCommandAsync(cadCommand, ct).ConfigureAwait(false);
        order.SetCadId(cadId);

        order.SetPrice(req.Price);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}

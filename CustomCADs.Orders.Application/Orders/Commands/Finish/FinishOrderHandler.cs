using CustomCADs.Orders.Application.Common.Exceptions;
using CustomCADs.Orders.Domain.Common;
using CustomCADs.Orders.Domain.Orders.Reads;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.Shared.UseCases.Cads.Commands;

namespace CustomCADs.Orders.Application.Orders.Commands.Finish;

public sealed class FinishOrderHandler(IOrderReads reads, IUnitOfWork uow, IRequestSender sender)
    : ICommandHandler<FinishOrderCommand>
{
    public async Task Handle(FinishOrderCommand req, CancellationToken ct)
    {
        Order order = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw OrderNotFoundException.ById(req.Id);

        if (req.DesignerId != order.DesignerId)
        {
            throw OrderAuthorizationException.NotAssociated("finish");
        }
        order.SetFinishedStatus();

        CreateCadCommand cadCommand = new(
            Key: req.Cad.Key,
            ContentType: req.Cad.ContentType
        );
        CadId cadId = await sender.SendCommandAsync(cadCommand, ct).ConfigureAwait(false);
        order.SetCadId(cadId);

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}

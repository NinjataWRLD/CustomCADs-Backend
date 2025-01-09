using CustomCADs.Orders.Domain.Common;
using CustomCADs.Orders.Domain.OngoingOrders.Reads;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.Shared.UseCases.Cads.Commands;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.Orders.Application.OngoingOrders.Commands.Status.Finish;

public sealed class FinishOngoingOrderHandler(IOngoingOrderReads reads, IUnitOfWork uow, IRequestSender sender)
    : ICommandHandler<FinishOngoingOrderCommand, FinishOngoingOrderDto>
{
    public async Task<FinishOngoingOrderDto> Handle(FinishOngoingOrderCommand req, CancellationToken ct)
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

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        GetCadPresignedUrlPostByIdQuery cadQuery = new(
            Name: order.Name,
            ContentType: req.Cad.ContentType,
            FileName: order.Name
        );
        var (Key, Url) = await sender.SendQueryAsync(cadQuery, ct).ConfigureAwait(false);

        return new(
            PresignedKey: Key,
            GeneratedUrl: Url
        );
    }
}

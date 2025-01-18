using CustomCADs.Orders.Domain.Common;
using CustomCADs.Orders.Domain.OngoingOrders.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Orders.Application.OngoingOrders.Commands.Status.Accept;

public sealed class AcceptOngoingOrderHandler(IOngoingOrderReads reads, IUnitOfWork uow, IRequestSender sender)
    : ICommandHandler<AcceptOngoingOrderCommand>
{
    public async Task Handle(AcceptOngoingOrderCommand req, CancellationToken ct)
    {
        OngoingOrder order = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw OngoingOrderNotFoundException.ById(req.Id);

        GetAccountExistsByIdQuery designerQuery = new(req.DesignerId);
        bool designerExists = await sender.SendQueryAsync(designerQuery, ct).ConfigureAwait(false);
        if (!designerExists)
        {
            throw OngoingOrderNotFoundException.DesignerId(req.DesignerId);
        }

        order.SetAcceptedStatus();
        order.SetDesignerId(req.DesignerId);

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}


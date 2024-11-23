using CustomCADs.Orders.Domain.Common;
using CustomCADs.Orders.Domain.Common.Exceptions.Orders;
using CustomCADs.Orders.Domain.Orders;
using CustomCADs.Orders.Domain.Orders.Enums;
using CustomCADs.Orders.Domain.Orders.Reads;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Cads;
using CustomCADs.Shared.UseCases.Cads.Commands;

namespace CustomCADs.Orders.Application.Orders.Commands.Finish;

public class FinishOrderHandler(IOrderReads reads, IUnitOfWork uow, IRequestSender sender)
    : ICommandHandler<FinishOrderCommand>
{
    public async Task Handle(FinishOrderCommand req, CancellationToken ct)
    {
        Order order = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw OrderNotFoundException.ById(req.Id);

        if (req.FinisherId != order.DesignerId)
        {
            throw OrderValidationException.Custom("Cannot finish an order you aren't associated with.");
        }
        order.SetFinishedStatus();

        if (order.DeliveryType is DeliveryType.Digital or DeliveryType.Both)
        {
            if (req.Cad is null)
            {
                throw OrderValidationException.Custom("Cannot finish a Digital delivery type order without providing a CadId.");
            }

            var (Key, ContentType) = req.Cad.Value;
            CreateCadCommand cadCommand = new(Key, ContentType);
            CadId cadId = await sender.SendCommandAsync(cadCommand, ct).ConfigureAwait(false);
            order.SetCadId(cadId);
        }

        await uow.SaveChangesAsync(ct);
    }
}

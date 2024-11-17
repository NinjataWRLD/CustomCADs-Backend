using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Cads;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Shipments;

namespace CustomCADs.Orders.Application.CustomOrders.Commands.Finish;

public record FinishCustomOrderCommand(
    CustomOrderId Id,
    UserId FinisherId,
    CadId? CadId,
    ShipmentId? ShipmentId
) : ICommand;

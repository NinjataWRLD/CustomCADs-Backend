using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Delivery.Application.Shipments.Commands.Delete;

public record DeleteShipmentCommand(
    ShipmentId Id,
    AccountId BuyerId
) : ICommand;

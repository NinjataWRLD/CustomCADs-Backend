using CustomCADs.Delivery.Domain.Shipments.ValueObjects;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Delivery.Application.Shipments.Commands.Edit;

public record EditShipmentCommand(
    ShipmentId Id,
    Address Address,
    AccountId BuyerId
) : ICommand;

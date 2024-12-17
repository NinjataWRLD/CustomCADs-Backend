namespace CustomCADs.Shared.UseCases.Shipments.Commands;

public sealed record CreateShipmentCommand(
    AccountId BuyerId
) : ICommand<ShipmentId>;

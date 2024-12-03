namespace CustomCADs.Shared.UseCases.Shipments.Commands;

public sealed record CreateShipmentCommand(
    AccountId ClientId
) : ICommand<ShipmentId>;

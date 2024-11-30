namespace CustomCADs.Shared.UseCases.Shipments.Commands;

public record CreateShipmentCommand(AccountId ClientId) : ICommand<ShipmentId>;

namespace CustomCADs.Shared.UseCases.Shipments.Commands;

public record CreateShipmentCommand(UserId ClientId) : ICommand<ShipmentId>;

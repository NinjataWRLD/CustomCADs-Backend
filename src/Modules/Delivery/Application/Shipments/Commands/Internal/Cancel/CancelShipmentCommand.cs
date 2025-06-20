namespace CustomCADs.Delivery.Application.Shipments.Commands.Internal.Cancel;

public record CancelShipmentCommand(
	ShipmentId Id,
	string Comment
) : ICommand;

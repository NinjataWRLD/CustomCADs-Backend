namespace CustomCADs.Shared.UseCases.Shipments.Commands;

public record CancelShipmentCommand(
	ShipmentId Id,
	string Comment
) : ICommand;

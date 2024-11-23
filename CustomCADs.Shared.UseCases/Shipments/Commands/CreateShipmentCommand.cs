using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Shipments;

namespace CustomCADs.Shared.UseCases.Shipments.Commands;

public record CreateShipmentCommand(UserId ClientId) : ICommand<ShipmentId>;

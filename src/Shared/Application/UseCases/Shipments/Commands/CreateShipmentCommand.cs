using CustomCADs.Shared.Application.Dtos.Delivery;

namespace CustomCADs.Shared.Application.UseCases.Shipments.Commands;

public sealed record CreateShipmentCommand(
	string Service,
	ShipmentInfoDto Info,
	AddressDto Address,
	ContactDto Contact,
	AccountId BuyerId
) : ICommand<ShipmentId>;

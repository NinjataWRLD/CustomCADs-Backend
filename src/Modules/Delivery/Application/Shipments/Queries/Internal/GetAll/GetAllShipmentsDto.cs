using CustomCADs.Delivery.Domain.Shipments.ValueObjects;

namespace CustomCADs.Delivery.Application.Shipments.Queries.Internal.GetAll;

public record GetAllShipmentsDto(
	ShipmentId Id,
	Address Address,
	DateTimeOffset RequestedAt,
	string BuyerName
);

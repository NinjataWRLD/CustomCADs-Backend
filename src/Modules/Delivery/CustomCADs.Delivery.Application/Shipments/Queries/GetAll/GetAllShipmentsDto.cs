using CustomCADs.Delivery.Domain.Shipments.ValueObjects;

namespace CustomCADs.Delivery.Application.Shipments.Queries.GetAll;

public record GetAllShipmentsDto(
    ShipmentId Id,
    Address Address,
    string BuyerName
);
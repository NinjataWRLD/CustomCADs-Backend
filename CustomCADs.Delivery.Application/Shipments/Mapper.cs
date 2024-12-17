using CustomCADs.Delivery.Application.Shipments.Queries.GetAll;
using CustomCADs.Delivery.Domain.Shipments;

namespace CustomCADs.Delivery.Application.Shipments;

public static class Mapper
{
    public static GetAllShipmentsDto ToGetAllShipmentsDto(this Shipment shipment)
        => new(
            Id: shipment.Id,
            ShipmentStatus: shipment.ShipmentStatus,
            Address: shipment.Address,
            ClientId: shipment.BuyerId
        );
}

using CustomCADs.Delivery.Application.Shipments.Queries.GetAll;

namespace CustomCADs.Delivery.Application.Shipments;

public static class Mapper
{
    public static GetAllShipmentsDto ToGetAllShipmentsDto(this Shipment shipment, string buyer)
        => new(
            Id: shipment.Id,
            Address: shipment.Address,
            BuyerName: buyer
        );
}

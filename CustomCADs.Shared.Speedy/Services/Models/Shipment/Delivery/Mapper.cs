using CustomCADs.Shared.Speedy.API.Dtos.Shipment.Delivery;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Delivery;

namespace CustomCADs.Shared.Speedy.Services.Models.Shipment.Delivery;

public static class Mapper
{

    public static ShipmentDeliveryModel ToModel(this ShipmentDeliveryDto dto)
        => new(
            Deadline: dto.Deadline,
            DeliveryDateTime: DateTime.Parse(dto.DeliveryDateTime),
            Consignee: dto.Consignee,
            DeliveryNote: dto.DeliveryNote
        );
}

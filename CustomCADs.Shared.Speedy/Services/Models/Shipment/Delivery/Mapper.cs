using CustomCADs.Shared.Speedy.API.Dtos.Shipment.Delivery;

namespace CustomCADs.Shared.Speedy.Services.Models.Shipment.Delivery;

internal static class Mapper
{

    internal static ShipmentDeliveryModel ToModel(this ShipmentDeliveryDto dto)
        => new(
            Deadline: dto.Deadline,
            DeliveryDateTime: DateTime.Parse(dto.DeliveryDateTime),
            Consignee: dto.Consignee,
            DeliveryNote: dto.DeliveryNote
        );
}

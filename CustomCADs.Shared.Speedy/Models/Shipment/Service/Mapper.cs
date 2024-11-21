using CustomCADs.Shared.Speedy.API.Dtos.ShipmentService;
using CustomCADs.Shared.Speedy.Models.Shipment.Service.AdditionalServices;

namespace CustomCADs.Shared.Speedy.Models.Shipment.Service;

public static class Mapper
{
    public static ShipmentServiceDto ToDto(this ShipmentServiceModel model)
        => new(
            ServiceId: model.ServiceId,
            PickupDate: model.PickupDate,
            SaturdayDelivery: model.SaturdayDelivery,
            AutoAdjustPickupDate: model.AutoAdjustPickupDate,
            DefferedValue: model.DefferedValue,
            AdditionalServices: model.AdditionalServices?.ToDto()
        );
}

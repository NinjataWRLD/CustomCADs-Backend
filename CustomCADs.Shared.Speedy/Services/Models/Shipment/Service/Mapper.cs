using CustomCADs.Shared.Speedy.API.Dtos.ShipmentService;
using CustomCADs.Shared.Speedy.Services.Models.Shipment;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Service;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Service.AdditionalServices;

namespace CustomCADs.Shared.Speedy.Services.Models.Shipment.Service;

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

    public static ShipmentServiceModel ToModel(this ShipmentServiceDto dto)
        => new(
            ServiceId: dto.ServiceId,
            PickupDate: dto.PickupDate,
            SaturdayDelivery: dto.SaturdayDelivery,
            AutoAdjustPickupDate: dto.AutoAdjustPickupDate,
            DefferedValue: dto.DefferedValue,
            AdditionalServices: dto.AdditionalServices?.ToModel()
        );
}

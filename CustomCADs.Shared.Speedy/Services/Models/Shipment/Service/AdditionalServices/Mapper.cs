using CustomCADs.Shared.Speedy.API.Dtos.ShipmentService.ShipmentAdditionalServices;
using CustomCADs.Shared.Speedy.Services.Models.Shipment;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Service;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Service.AdditionalServices;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Service.AdditionalServices.Cod;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Service.AdditionalServices.DeclaredValue;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Service.AdditionalServices.Obpd;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Service.AdditionalServices.Return;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Service.AdditionalServices;

namespace CustomCADs.Shared.Speedy.Services.Models.Shipment.Service.AdditionalServices;

public static class Mapper
{
    public static ShipmentAdditionalServicesDto ToDto(this ShipmentAdditionalServicesModel model)
        => new(
            Cod: model.Cod?.ToDto(),
            Obdp: model.Obdp?.ToDto(),
            DeclaredValue: model.DeclaredValue?.ToDto(),
            Returns: model.Returns?.ToDto(),
            FixedTimeDelivery: model.FixedTimeDelivery,
            SpecialDeliveryId: model.SpecialDeliveryId,
            DeliveryToFloor: model.DeliveryToFloor
        );

    public static ShipmentAdditionalServicesModel ToModel(this ShipmentAdditionalServicesDto dto)
        => new(
            Cod: dto.Cod?.ToModel(),
            Obdp: dto.Obdp?.ToModel(),
            DeclaredValue: dto.DeclaredValue?.ToModel(),
            Returns: dto.Returns?.ToModel(),
            FixedTimeDelivery: dto.FixedTimeDelivery,
            SpecialDeliveryId: dto.SpecialDeliveryId,
            DeliveryToFloor: dto.DeliveryToFloor
        );
}

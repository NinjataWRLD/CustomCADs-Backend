using CustomCADs.Shared.Speedy.API.Dtos.ShipmentService.ShipmentAdditionalServices;
using CustomCADs.Shared.Speedy.Models.Shipment.Service.AdditionalServices.Cod;
using CustomCADs.Shared.Speedy.Models.Shipment.Service.AdditionalServices.DeclaredValue;
using CustomCADs.Shared.Speedy.Models.Shipment.Service.AdditionalServices.Obpd;
using CustomCADs.Shared.Speedy.Models.Shipment.Service.AdditionalServices.Return;

namespace CustomCADs.Shared.Speedy.Models.Shipment.Service.AdditionalServices;

public static class Mapper
{
    public static ShipmentAdditionalServicesDto ToDto(this ShipmentAdditionalServicesModel model)
        => new(
            Cod: model.Cod?.ToDto(),
            Obdp: model.Obdp?.ToDto(),
            DeclaredValue: model.DeclaredValue?.ToDto(),
            FixedTimeDelivery: model.FixedTimeDelivery,
            Returns: model.Returns?.ToDto(),
            SpecialDeliveryId: model.SpecialDeliveryId,
            DeliveryToFloor: model.DeliveryToFloor
        );
}

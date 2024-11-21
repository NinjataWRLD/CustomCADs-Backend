using CustomCADs.Shared.Speedy.Models.Shipment.Service.AdditionalServices.Cod;
using CustomCADs.Shared.Speedy.Models.Shipment.Service.AdditionalServices.DeclaredValue;
using CustomCADs.Shared.Speedy.Models.Shipment.Service.AdditionalServices.Obpd;
using CustomCADs.Shared.Speedy.Models.Shipment.Service.AdditionalServices.Return;

namespace CustomCADs.Shared.Speedy.Models.Shipment.Service.AdditionalServices;

public record ShipmentAdditionalServicesModel(
    ShipmentCodAdditionalServiceModel? Cod,
    ShipmentObpdModel? Obdp,
    ShipmentDeclaredValueAdditionalServiceModel? DeclaredValue,
    ShipmentReturnAdditionalServiceModel? Returns,
    int? FixedTimeDelivery,
    int? SpecialDeliveryId,
    int? DeliveryToFloor
);
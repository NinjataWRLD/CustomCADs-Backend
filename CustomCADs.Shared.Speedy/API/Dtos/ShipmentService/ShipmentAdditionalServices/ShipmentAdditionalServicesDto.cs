namespace CustomCADs.Shared.Speedy.API.Dtos.ShipmentService.ShipmentAdditionalServices;

using ShipmentCodAdditionalService;
using ShipmentDeclaredValueAdditionalService;
using ShipmentObpd;
using ShipmentReturnAdditionalServices;

public record ShipmentAdditionalServicesDto(
    ShipmentCodAdditionalServiceDto? Cod,
    ShipmentObpdDto? Obdp,
    ShipmentDeclaredValueAdditionalServiceDto? DeclaredValue,
    int? FixedTimeDelivery,
    ShipmentReturnAdditionalServicesDto? Returns,
    int? SpecialDeliveryId,
    int? DeliveryToFloor
);
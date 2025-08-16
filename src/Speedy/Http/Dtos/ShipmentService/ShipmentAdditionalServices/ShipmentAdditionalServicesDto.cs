namespace CustomCADs.Speedy.Http.Dtos.ShipmentService.ShipmentAdditionalServices;

using ShipmentCodAdditionalService;
using ShipmentDeclaredValueAdditionalService;
using ShipmentObpd;
using ShipmentReturnAdditionalServices;

internal record ShipmentAdditionalServicesDto(
	ShipmentCodAdditionalServiceDto? Cod,
	ShipmentObpdDto? Obdp,
	ShipmentDeclaredValueAdditionalServiceDto? DeclaredValue,
	int? FixedTimeDelivery,
	ShipmentReturnAdditionalServicesDto? Returns,
	int? SpecialDeliveryId,
	int? DeliveryToFloor
);

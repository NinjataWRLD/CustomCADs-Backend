using CustomCADs.Speedy.Core.Services.Models.Shipment.Service.AdditionalServices.Cod;
using CustomCADs.Speedy.Core.Services.Models.Shipment.Service.AdditionalServices.DeclaredValue;
using CustomCADs.Speedy.Core.Services.Models.Shipment.Service.AdditionalServices.Obpd;
using CustomCADs.Speedy.Core.Services.Models.Shipment.Service.AdditionalServices.Return;

namespace CustomCADs.Speedy.Core.Services.Models.Shipment.Service.AdditionalServices;

public record ShipmentAdditionalServicesModel(
	ShipmentCodAdditionalServiceModel? Cod,
	ShipmentObpdModel? Obdp,
	ShipmentDeclaredValueAdditionalServiceModel? DeclaredValue,
	ShipmentReturnAdditionalServicesModel? Returns,
	int? FixedTimeDelivery,
	int? SpecialDeliveryId,
	int? DeliveryToFloor
);

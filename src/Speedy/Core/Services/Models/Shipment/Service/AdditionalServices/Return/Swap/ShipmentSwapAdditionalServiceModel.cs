namespace CustomCADs.Speedy.Core.Services.Models.Shipment.Service.AdditionalServices.Return.Swap;

public record ShipmentSwapAdditionalServiceModel(
	int ServiceId,
	int ParcelsCount,
	double? DeclaredValue,
	bool? Fragile,
	int? ReturnToOfficeId,
	bool? ThirdPartyPayer
);

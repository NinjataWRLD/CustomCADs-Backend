namespace CustomCADs.Speedy.Core.Models.Shipment.Service.AdditionalServices.Return.Receipt;

public record ShipmentReturnReceiptAdditionalServiceModel(
	bool Enabled,
	long? ReturnToClientId,
	int? ReturnToOfficeId,
	bool? ThirdPartyPayer
);

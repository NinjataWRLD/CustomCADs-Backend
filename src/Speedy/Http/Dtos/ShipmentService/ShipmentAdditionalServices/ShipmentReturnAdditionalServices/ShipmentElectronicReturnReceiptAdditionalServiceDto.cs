namespace CustomCADs.Speedy.Http.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentReturnAdditionalServices;

internal record ShipmentElectronicReturnReceiptAdditionalServiceDto(
	string[] RecipientEmails,
	bool? ThirdPartyPayer
);

namespace CustomCADs.Speedy.Core.Services.Models.Shipment.Service.AdditionalServices.Return.ElectronicReceipt;

public record ShipmentElectronicReturnReceiptAdditionalServiceModel(
	string[] RecipientEmails,
	bool? ThirdPartyPayer
);

namespace CustomCADs.Speedy.Core.Models.Shipment.Service.AdditionalServices.Return.ElectronicReceipt;

public record ShipmentElectronicReturnReceiptAdditionalServiceModel(
	string[] RecipientEmails,
	bool? ThirdPartyPayer
);

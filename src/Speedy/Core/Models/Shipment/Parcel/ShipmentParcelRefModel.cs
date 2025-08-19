namespace CustomCADs.Speedy.Core.Models.Shipment.Parcel;

public record ShipmentParcelRefModel(
	string? Id,
	string? ExternalCarrierParcelNumber,
	string? FullBarcode
);

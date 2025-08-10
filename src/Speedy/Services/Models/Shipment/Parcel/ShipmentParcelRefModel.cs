namespace CustomCADs.Speedy.Services.Models.Shipment.Parcel;

public record ShipmentParcelRefModel(
	string? Id,
	string? ExternalCarrierParcelNumber,
	string? FullBarcode
);

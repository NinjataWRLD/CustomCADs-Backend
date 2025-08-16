namespace CustomCADs.Speedy.Core.Services.Models.Shipment.Parcel;

public record ShipmentParcelRefModel(
	string? Id,
	string? ExternalCarrierParcelNumber,
	string? FullBarcode
);

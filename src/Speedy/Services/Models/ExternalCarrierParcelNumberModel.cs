namespace CustomCADs.Speedy.Services.Models;

public record ExternalCarrierParcelNumberModel(
	Carrier ExternalCarrier,
	string ParcelNumber
);

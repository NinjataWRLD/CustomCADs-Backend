namespace CustomCADs.Speedy.Core.Services.Models;

public record ExternalCarrierParcelNumberModel(
	Carrier ExternalCarrier,
	string ParcelNumber
);

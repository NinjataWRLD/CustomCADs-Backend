namespace CustomCADs.Speedy.Core.Models;

public record ExternalCarrierParcelNumberModel(
	Carrier ExternalCarrier,
	string ParcelNumber
);

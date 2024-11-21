namespace CustomCADs.Shared.Speedy.Models;

public record ExternalCarrierParcelNumberModel(
    Carrier ExternalCarrier, 
    string ParcelNumber
);
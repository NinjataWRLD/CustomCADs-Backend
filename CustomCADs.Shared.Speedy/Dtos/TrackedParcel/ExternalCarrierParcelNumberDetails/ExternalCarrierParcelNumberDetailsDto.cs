namespace CustomCADs.Shared.Speedy.Dtos.TrackedParcel.ExternalCarrierParcelNumberDetails;

public record ExternalCarrierParcelNumberDetailsDto(
    int ExternalCarrierId,
    string ExternalCarrierName,
    string? TrackAndTraceUrl
);
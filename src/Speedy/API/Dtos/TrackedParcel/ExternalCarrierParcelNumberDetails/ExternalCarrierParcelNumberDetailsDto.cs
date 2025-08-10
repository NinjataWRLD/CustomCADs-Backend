namespace CustomCADs.Speedy.API.Dtos.TrackedParcel.ExternalCarrierParcelNumberDetails;

public record ExternalCarrierParcelNumberDetailsDto(
	int ExternalCarrierId,
	string ExternalCarrierName,
	string? TrackAndTraceUrl
);

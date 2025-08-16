namespace CustomCADs.Speedy.Http.Dtos.TrackedParcel.ExternalCarrierParcelNumberDetails;

internal record ExternalCarrierParcelNumberDetailsDto(
	int ExternalCarrierId,
	string ExternalCarrierName,
	string? TrackAndTraceUrl
);

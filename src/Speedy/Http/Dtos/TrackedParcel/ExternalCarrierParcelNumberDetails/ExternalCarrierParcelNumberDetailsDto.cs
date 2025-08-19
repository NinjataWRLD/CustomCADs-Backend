namespace CustomCADs.Speedy.Http.Dtos.TrackedParcel.ExternalCarrierParcelNumberDetails;

/// <param name="ExternalCarrierId">
///     External carrier id.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="ExternalCarrierName">
///     External carrier name.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="TrackAndTraceUrl">
///     Track and trace URL for the external carrier parcel, if available.
///     <br />
///     Required: No.
/// </param>
internal record ExternalCarrierParcelNumberDetailsDto(
	int ExternalCarrierId,
	string ExternalCarrierName,
	string? TrackAndTraceUrl
);

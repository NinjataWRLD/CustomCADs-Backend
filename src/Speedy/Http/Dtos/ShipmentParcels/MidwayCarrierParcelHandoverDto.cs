namespace CustomCADs.Speedy.Http.Dtos.ShipmentParcels;

/// <param name="Id">
///     Parcel identifier.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="CountryId">
///     Country ISO code for handover.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="DateTime">
///     Handover date and time (ISO 8601 format).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="SiteName">
///     Site name for handover, if applicable.
///     <br />
///     Required: No.
/// </param>
internal record MidwayCarrierParcelHandoverDto(
	string Id,
	long CountryId,
	string DateTime,
	string? SiteName
);

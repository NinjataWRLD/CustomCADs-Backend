namespace CustomCADs.Speedy.Http.Dtos.ParcelToPrint;

/// <param name="ParcelId">
///     Parcel id for the label.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="FullBarcode">
///     Full barcode for the parcel.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="ExportPriority">
///     Export priority for the parcel.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="HubId">
///     Hub id for the parcel, if applicable.
///     <br />
///     Required: No.
/// </param>
/// <param name="OfficeId">
///     Office id for the parcel, if applicable.
///     <br />
///     Required: No.
/// </param>
/// <param name="OfficeName">
///     Office name for the parcel, if applicable.
///     <br />
///     Required: No.
/// </param>
/// <param name="DeadlineDay">
///     Deadline day for the parcel, if applicable.
///     <br />
///     Required: No.
/// </param>
/// <param name="DeadlineMonth">
///     Deadline month for the parcel, if applicable.
///     <br />
///     Required: No.
/// </param>
/// <param name="TourId">
///     Tour id for the parcel, if applicable.
///     <br />
///     Required: No.
/// </param>
internal record LabelInfoDto(
	string ParcelId,
	string FullBarcode,
	int ExportPriority,
	int? HubId,
	int? OfficeId,
	string? OfficeName,
	int? DeadlineDay,
	int? DeadlineMonth,
	int? TourId
);

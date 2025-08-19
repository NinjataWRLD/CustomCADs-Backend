namespace CustomCADs.Speedy.Http.Dtos.BulkTrackingDataFile;

/// <param name="Id">
///     Bulk tracking data file id.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="Url">
///     URL to access the bulk tracking data file.
///     <br />
///     Required: Yes.
/// </param>
internal record BulkTrackingDataFileDto(
	long Id,
	string Url
);

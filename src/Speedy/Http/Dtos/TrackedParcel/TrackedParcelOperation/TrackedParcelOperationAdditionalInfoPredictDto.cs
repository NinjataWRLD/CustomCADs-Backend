namespace CustomCADs.Speedy.Http.Dtos.TrackedParcel.TrackedParcelOperation;

/// <param name="PredictedVisitDateTimeFrom">
///     Predicted visit start date and time (ISO 8601 format).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="PredictedVisitDateTimeTo">
///     Predicted visit end date and time (ISO 8601 format).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="Canceled">
///     Flag indicating if the predicted visit was canceled.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="IncludedDelayInMinutes">
///     Included delay in minutes, if any.
///     <br />
///     Required: No.
/// </param>
internal record TrackedParcelOperationAdditionalInfoPredictDto(
	string PredictedVisitDateTimeFrom,
	string PredictedVisitDateTimeTo,
	bool Canceled,
	int? IncludedDelayInMinutes
);

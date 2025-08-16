namespace CustomCADs.Speedy.Http.Dtos.TrackedParcel.TrackedParcelOperation;

internal record TrackedParcelOperationAdditionalInfoPredictDto(
	string PredictedVisitDateTimeFrom,
	string PredictedVisitDateTimeTo,
	bool Canceled,
	int? IncludedDelayInMinutes
);

namespace CustomCADs.Speedy.Core.Contracts.Track;

public record TrackedParcelOperationAdditionalInfoPredictModel(
	DateTime PredictedVisitDateTimeFrom,
	DateTime PredictedVisitDateTimeTo,
	bool Canceled,
	int? IncludedDelayInMinutes
);

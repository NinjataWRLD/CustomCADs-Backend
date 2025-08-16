namespace CustomCADs.Speedy.Core.Services.Track.Models;

public record TrackedParcelOperationAdditionalInfoPredictModel(
	DateTime PredictedVisitDateTimeFrom,
	DateTime PredictedVisitDateTimeTo,
	bool Canceled,
	int? IncludedDelayInMinutes
);

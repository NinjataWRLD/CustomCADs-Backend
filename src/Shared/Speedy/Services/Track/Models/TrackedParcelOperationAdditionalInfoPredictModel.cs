namespace CustomCADs.Shared.Speedy.Services.Track.Models;

public record TrackedParcelOperationAdditionalInfoPredictModel(
    DateTime PredictedVisitDateTimeFrom,
    DateTime PredictedVisitDateTimeTo,
    bool Canceled,
    int? IncludedDelayInMinutes
);
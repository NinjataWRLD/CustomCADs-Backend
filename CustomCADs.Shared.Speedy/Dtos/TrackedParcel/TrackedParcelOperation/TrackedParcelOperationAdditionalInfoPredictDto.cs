namespace CustomCADs.Shared.Speedy.Dtos.TrackedParcel.TrackedParcelOperation;

public record TrackedParcelOperationAdditionalInfoPredictDto(
    string PredictedVisitDateTimeFrom,
    string PredictedVisitDateTimeTo,
    bool Canceled,
    int? IncludedDelayInMinutes
);
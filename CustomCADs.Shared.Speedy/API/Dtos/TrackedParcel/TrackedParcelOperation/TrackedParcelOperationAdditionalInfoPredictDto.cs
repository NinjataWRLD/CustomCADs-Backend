namespace CustomCADs.Shared.Speedy.API.Dtos.TrackedParcel.TrackedParcelOperation;

public record TrackedParcelOperationAdditionalInfoPredictDto(
    string PredictedVisitDateTimeFrom,
    string PredictedVisitDateTimeTo,
    bool Canceled,
    int? IncludedDelayInMinutes
);
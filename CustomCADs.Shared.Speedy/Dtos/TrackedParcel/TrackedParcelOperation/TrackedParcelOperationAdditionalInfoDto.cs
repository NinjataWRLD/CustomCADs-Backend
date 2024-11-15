namespace CustomCADs.Shared.Speedy.Dtos.TrackedParcel.TrackedParcelOperation;

public record TrackedParcelOperationAdditionalInfoDto(
    string? OfficeURL,
    string? GeoPUDOId,
    TrackedParcelOperationAdditionalInfoPredictDto? Predict
);
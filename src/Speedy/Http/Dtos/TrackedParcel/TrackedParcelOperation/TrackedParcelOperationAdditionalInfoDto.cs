namespace CustomCADs.Speedy.Http.Dtos.TrackedParcel.TrackedParcelOperation;

internal record TrackedParcelOperationAdditionalInfoDto(
	string? OfficeURL,
	string? GeoPUDOId,
	TrackedParcelOperationAdditionalInfoPredictDto? Predict
);

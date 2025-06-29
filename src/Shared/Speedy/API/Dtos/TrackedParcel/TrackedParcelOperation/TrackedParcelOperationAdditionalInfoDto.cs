﻿namespace CustomCADs.Shared.Speedy.API.Dtos.TrackedParcel.TrackedParcelOperation;

public record TrackedParcelOperationAdditionalInfoDto(
	string? OfficeURL,
	string? GeoPUDOId,
	TrackedParcelOperationAdditionalInfoPredictDto? Predict
);

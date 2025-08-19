namespace CustomCADs.Speedy.Http.Dtos.TrackedParcel.TrackedParcelOperation;

/// <param name="OfficeURL">
///     URL for the office related to the operation, if available.
///     <br />
///     Required: No.
/// </param>
/// <param name="GeoPUDOId">
///     GeoPUDO id for the operation, if available.
///     <br />
///     Required: No.
/// </param>
/// <param name="Predict">
///     Prediction details for the operation, if available.
///     <br />
///     Required: No. See TrackedParcelOperationAdditionalInfoPredictDto for details.
/// </param>
internal record TrackedParcelOperationAdditionalInfoDto(
	string? OfficeURL,
	string? GeoPUDOId,
	TrackedParcelOperationAdditionalInfoPredictDto? Predict
);

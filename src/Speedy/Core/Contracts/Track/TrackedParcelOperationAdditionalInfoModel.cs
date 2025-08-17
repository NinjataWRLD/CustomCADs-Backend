namespace CustomCADs.Speedy.Core.Contracts.Track;

public record TrackedParcelOperationAdditionalInfoModel(
	string? OfficeUrl,
	string? GeoPudoId,
	TrackedParcelOperationAdditionalInfoPredictModel? Predict
);

namespace CustomCADs.Speedy.Services.Track.Models;

public record TrackedParcelOperationAdditionalInfoModel(
	string? OfficeUrl,
	string? GeoPudoId,
	TrackedParcelOperationAdditionalInfoPredictModel? Predict
);

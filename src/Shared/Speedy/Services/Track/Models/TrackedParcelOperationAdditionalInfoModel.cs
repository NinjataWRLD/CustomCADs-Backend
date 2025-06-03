namespace CustomCADs.Shared.Speedy.Services.Track.Models;

public record TrackedParcelOperationAdditionalInfoModel(
	string? OfficeUrl,
	string? GeoPudoId,
	TrackedParcelOperationAdditionalInfoPredictModel? Predict
);

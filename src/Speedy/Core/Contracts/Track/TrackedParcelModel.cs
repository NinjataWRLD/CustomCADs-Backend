namespace CustomCADs.Speedy.Core.Contracts.Track;

public record TrackedParcelModel(
	string ParcelId,
	string[]? ExternalCarrierParcelNumbers,
	TrackedParcelOperationModel[] Operations,
	Dictionary<string, (int ExternalCarrierId, string ExternalCarrierName, string? TrackAndTraceUrl)>? ExternalCarrierParcelNumbersDetails
);

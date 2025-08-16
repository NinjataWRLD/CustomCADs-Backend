namespace CustomCADs.Speedy.Http.Dtos.TrackedParcel;

using Errors;
using ExternalCarrierParcelNumberDetails;
using TrackedParcelOperation;

internal record TrackedParcelDto(
	string ParcelId,
	string[]? ExternalCarrierParcelNumbers,
	TrackedParcelOperationDto[] Operations,
	Dictionary<string, ExternalCarrierParcelNumberDetailsDto>? ExternalCarrierParcelNumbersDetails,
	ErrorDto? Error
);

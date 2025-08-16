namespace CustomCADs.Speedy.Http.Dtos.TrackedParcel.TrackedParcelOperation;

internal record TrackedParcelOperationDto(
	string DateTime,
	int OperationCode,
	string Description,
	string? Place,
	string? Comment,
	string[] ExceptionCodes,
	string? ReturnShipmentId,
	string? RedirectShipmentId,
	string? Consignee,
	string? PodImageURL,
	TrackedParcelOperationAdditionalInfoDto? AdditionalInfo
);

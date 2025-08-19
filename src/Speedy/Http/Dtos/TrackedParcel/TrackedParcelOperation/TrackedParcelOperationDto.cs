namespace CustomCADs.Speedy.Http.Dtos.TrackedParcel.TrackedParcelOperation;

/// <param name="DateTime">
///     Date and time of the operation (ISO 8601 format).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="OperationCode">
///     Operation code.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="Description">
///     Description of the operation.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="Place">
///     Place of the operation, if available.
///     <br />
///     Required: No.
/// </param>
/// <param name="Comment">
///     Comment for the operation, if available.
///     <br />
///     Required: No.
/// </param>
/// <param name="ExceptionCodes">
///     Exception codes for the operation, if any.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="ReturnShipmentId">
///     Return shipment id, if applicable.
///     <br />
///     Required: No.
/// </param>
/// <param name="RedirectShipmentId">
///     Redirect shipment id, if applicable.
///     <br />
///     Required: No.
/// </param>
/// <param name="Consignee">
///     Consignee for the operation, if available.
///     <br />
///     Required: No.
/// </param>
/// <param name="PodImageURL">
///     POD image URL, if available.
///     <br />
///     Required: No.
/// </param>
/// <param name="AdditionalInfo">
///     Additional info for the operation, if available.
///     <br />
///     Required: No. See TrackedParcelOperationAdditionalInfoDto for details.
/// </param>
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

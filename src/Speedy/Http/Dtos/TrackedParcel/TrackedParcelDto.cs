namespace CustomCADs.Speedy.Http.Dtos.TrackedParcel;

using Errors;
using ExternalCarrierParcelNumberDetails;
using TrackedParcelOperation;

/// <param name="ParcelId">
///     Parcel id for the tracked parcel.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="ExternalCarrierParcelNumbers">
///     List of external carrier parcel numbers, if available.
///     <br />
///     Required: No.
/// </param>
/// <param name="Operations">
///     List of operations for the tracked parcel.
///     <br />
///     Required: Yes. See TrackedParcelOperationDto for details.
/// </param>
/// <param name="ExternalCarrierParcelNumbersDetails">
///     Details for external carrier parcel numbers, if available.
///     <br />
///     Required: No. See ExternalCarrierParcelNumberDetailsDto for details.
/// </param>
/// <param name="Error">
///     Error details for the tracked parcel, if any.
///     <br />
///     Required: No. See ErrorDto for details.
/// </param>
internal record TrackedParcelDto(
	string ParcelId,
	string[]? ExternalCarrierParcelNumbers,
	TrackedParcelOperationDto[] Operations,
	Dictionary<string, ExternalCarrierParcelNumberDetailsDto>? ExternalCarrierParcelNumbersDetails,
	ErrorDto? Error
);

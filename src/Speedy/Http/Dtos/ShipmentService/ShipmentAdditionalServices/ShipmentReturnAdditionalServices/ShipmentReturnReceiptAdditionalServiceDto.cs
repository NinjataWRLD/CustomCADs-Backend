namespace CustomCADs.Speedy.Http.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentReturnAdditionalServices;

/// <param name="Enabled">
///     Flag indicating if the return receipt is enabled.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="ReturnToClientId">
///     Client id to return the receipt to, if applicable.
///     <br />
///     Required: No.
/// </param>
/// <param name="ReturnToOfficeId">
///     Office id to return the receipt to, if applicable.
///     <br />
///     Required: No.
/// </param>
/// <param name="ThirdPartyPayer">
///     Flag indicating if a third party is the payer.
///     <br />
///     Required: No.
/// </param>
internal record ShipmentReturnReceiptAdditionalServiceDto(
	bool Enabled,
	long? ReturnToClientId,
	int? ReturnToOfficeId,
	bool? ThirdPartyPayer
);

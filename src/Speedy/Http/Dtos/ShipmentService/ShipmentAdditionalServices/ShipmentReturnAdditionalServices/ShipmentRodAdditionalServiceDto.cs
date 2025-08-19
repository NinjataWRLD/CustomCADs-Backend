namespace CustomCADs.Speedy.Http.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentReturnAdditionalServices;

/// <param name="Enabled">
///     Flag indicating if the ROD additional service is enabled.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="Comment">
///     Comment for the ROD additional service, if applicable.
///     <br />
///     Required: No.
/// </param>
/// <param name="ReturnToClientId">
///     Client id to return the document to, if applicable.
///     <br />
///     Required: No.
/// </param>
/// <param name="ReturnToOfficeId">
///     Office id to return the document to, if applicable.
///     <br />
///     Required: No.
/// </param>
/// <param name="ThirdPartyPayer">
///     Flag indicating if a third party is the payer.
///     <br />
///     Required: No.
/// </param>
internal record ShipmentRodAdditionalServiceDto(
	bool Enabled,
	string? Comment,
	long? ReturnToClientId,
	int? ReturnToOfficeId,
	bool? ThirdPartyPayer
);

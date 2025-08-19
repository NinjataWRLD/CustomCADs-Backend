namespace CustomCADs.Speedy.Http.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentReturnAdditionalServices;

/// <param name="ServiceId">
///     Service id for the swap additional service.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="ParcelsCount">
///     Number of parcels for the swap additional service.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="DeclaredValue">
///     Declared value for the swap additional service, if applicable.
///     <br />
///     Required: No.
/// </param>
/// <param name="Fragile">
///     Fragile flag for the swap additional service, if applicable.
///     <br />
///     Required: No.
/// </param>
/// <param name="ReturnToOfficeId">
///     Office id to return the swap to, if applicable.
///     <br />
///     Required: No.
/// </param>
/// <param name="ThirdPartyPayer">
///     Flag indicating if a third party is the payer.
///     <br />
///     Required: No.
/// </param>
internal record ShipmentSwapAdditionalServiceDto(
	int ServiceId,
	int ParcelsCount,
	double? DeclaredValue,
	bool? Fragile,
	int? ReturnToOfficeId,
	bool? ThirdPartyPayer
);

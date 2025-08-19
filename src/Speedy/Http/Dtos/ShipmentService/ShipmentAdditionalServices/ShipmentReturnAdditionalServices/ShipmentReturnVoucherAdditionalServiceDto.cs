namespace CustomCADs.Speedy.Http.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentReturnAdditionalServices;

/// <param name="ServiceId">
///     Service id for the return voucher additional service.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="Payer">
///     Payer for the return voucher additional service.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="ValidityPeriod">
///     Validity period for the return voucher, if applicable.
///     <br />
///     Required: No.
/// </param>
internal record ShipmentReturnVoucherAdditionalServiceDto(
	int ServiceId,
	Payer Payer,
	int? ValidityPeriod
);

namespace CustomCADs.Speedy.Http.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentObpd;

/// <param name="Option">
///     Defines the option to be used: OPEN or TEST (open parcels or open and test before payment).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="ReturnShipmentServiceId">
///     Service id to be used on return.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="ReturnShipmentPayer">
///     Who pays the return shipment. Allowed: SENDER, RECIPIENT, THIRD_PARTY.
///     <br />
///     Required: Yes.
/// </param>
internal record ShipmentObpdDto(
	ObpdOption Option,
	int ReturnShipmentServiceId,
	Payer ReturnShipmentPayer
);

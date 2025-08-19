namespace CustomCADs.Speedy.Http.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentReturnAdditionalServices.ShipmentRopAdditionalService;

/// <param name="ServiceId">
///     Service id for the ROP additional service line.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="ParcelsCount">
///     Number of parcels for the ROP additional service line.
///     <br />
///     Required: Yes.
/// </param>
internal record ShipmentRopAdditionalServiceLineDto(
	int ServiceId,
	int ParcelsCount
);

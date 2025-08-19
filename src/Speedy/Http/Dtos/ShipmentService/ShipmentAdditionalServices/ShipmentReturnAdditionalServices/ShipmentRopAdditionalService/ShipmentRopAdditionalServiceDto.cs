namespace CustomCADs.Speedy.Http.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentReturnAdditionalServices.ShipmentRopAdditionalService;

/// <param name="Pallets">
///     List of pallet lines for the ROP additional service.
///     <br />
///     Required: Yes. See ShipmentRopAdditionalServiceLineDto for details.
/// </param>
/// <param name="ThirdPartyPayer">
///     Flag indicating if a third party is the payer.
///     <br />
///     Required: No.
/// </param>
internal record ShipmentRopAdditionalServiceDto(
	ShipmentRopAdditionalServiceLineDto[] Pallets,
	bool? ThirdPartyPayer
);

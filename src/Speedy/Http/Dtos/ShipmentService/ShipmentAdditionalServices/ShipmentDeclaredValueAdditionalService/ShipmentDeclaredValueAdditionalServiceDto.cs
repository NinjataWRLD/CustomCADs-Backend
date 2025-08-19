namespace CustomCADs.Speedy.Http.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentDeclaredValueAdditionalService;

/// <param name="Amount">
///     Declared value base amount (extended liability).
///     <br />
///     Required: Yes. Always in local system currency.
/// </param>
/// <param name="Fragile">
///     Fragile flag for shipment content.
///     <br />
///     Required: No. Fragile shipments require declared value.
/// </param>
/// <param name="IgnoreIfNotApplicable">
///     Flag to ignore declared value if not applicable (used in calculation service only).
///     <br />
///     Required: No. Used to provide price to end clients without repeating calculation request.
/// </param>
internal record ShipmentDeclaredValueAdditionalServiceDto(
	double Amount,
	bool? Fragile,
	bool? IgnoreIfNotApplicable
);

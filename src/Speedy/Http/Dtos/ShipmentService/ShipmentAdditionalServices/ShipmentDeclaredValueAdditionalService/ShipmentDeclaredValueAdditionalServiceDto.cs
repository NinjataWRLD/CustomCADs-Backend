namespace CustomCADs.Speedy.Http.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentDeclaredValueAdditionalService;

internal record ShipmentDeclaredValueAdditionalServiceDto(
	double Amount,
	bool? Fragile,
	bool? IgnoreIfNotApplicable
);

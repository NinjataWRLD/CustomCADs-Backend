namespace CustomCADs.Speedy.Http.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentReturnAdditionalServices;

internal record ShipmentReturnVoucherAdditionalServiceDto(
	int ServiceId,
	Payer Payer,
	int? ValidityPeriod
);

namespace CustomCADs.Speedy.Http.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentCodAdditionalService;

internal record ShipmentCodAdditionalServiceDto(
	double Amount,
	string CurrencyCode,
	bool? PayoutToThirdParty,
	bool? PayoutToLoggedClient,
	bool? IncludeShippingPrice,
	bool? CardPaymentForbidden,
	ProcessingType? ProcessingType,
	ShipmentCodFiscalReceiptItemDto[]? FiscalReceiptItems
);

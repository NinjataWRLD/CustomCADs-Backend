namespace CustomCADs.Speedy.Http.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentCodAdditionalService;

internal record ShipmentCodFiscalReceiptItemDto(
	string Description,
	string VatGroup,
	double Amount,
	double AmountWithVat
);

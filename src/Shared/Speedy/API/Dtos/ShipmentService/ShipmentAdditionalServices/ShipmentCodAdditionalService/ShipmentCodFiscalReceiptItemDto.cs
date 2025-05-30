namespace CustomCADs.Shared.Speedy.API.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentCodAdditionalService;

public record ShipmentCodFiscalReceiptItemDto(
	string Description,
	string VatGroup,
	double Amount,
	double AmountWithVat
);

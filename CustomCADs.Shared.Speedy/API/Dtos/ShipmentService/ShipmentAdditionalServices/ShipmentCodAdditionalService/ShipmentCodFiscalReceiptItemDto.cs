namespace CustomCADs.Shared.Speedy.API.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentCodAdditionalService;

public record ShipmentCODFiscalReceiptItemDto(
    string Description,
    string VatGroup,
    double Amount,
    double AmountWithVat
);
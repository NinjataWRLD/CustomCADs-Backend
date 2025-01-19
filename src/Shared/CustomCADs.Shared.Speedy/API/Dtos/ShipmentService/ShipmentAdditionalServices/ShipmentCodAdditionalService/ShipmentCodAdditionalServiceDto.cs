namespace CustomCADs.Shared.Speedy.API.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentCodAdditionalService;

public record ShipmentCodAdditionalServiceDto(
    double Amount,
    string CurrencyCode,
    bool? PayoutToThirdParty,
    bool? PayoutToLoggedClient,
    bool? IncludeShippingPrice,
    bool? CardPaymentForbidden,
    ProcessingType? ProcessingType,
    ShipmentCodFiscalReceiptItemDto[]? FiscalReceiptItems
);
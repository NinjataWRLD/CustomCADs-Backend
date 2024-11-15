namespace CustomCADs.Shared.Speedy.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentCodAdditionalService;

using Enums;

public record ShipmentCodAdditionalServiceDto(
    double Amount,
    string CurrencyCode,
    bool? PayoutToThirdParty,
    bool? PayoutToLoggedClient,
    bool? IncludeShippingPrice,
    bool? CardPaymentForbidden,
    ShipmentCODFiscalReceiptItemDto[] FiscalReceiptItems,
    ProcessingType ProcessingType = ProcessingType.CASH
);
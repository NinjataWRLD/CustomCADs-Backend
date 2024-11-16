namespace CustomCADs.Shared.Speedy.API.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentCodAdditionalService;

using Enums;

public record ShipmentCodAdditionalServiceDto(
    double Amount,
    string CurrencyCode,
    bool? PayoutToThirdParty,
    bool? PayoutToLoggedClient,
    bool? IncludeShippingPrice,
    bool? CardPaymentForbidden,
    ProcessingType? ProcessingType,
    ShipmentCODFiscalReceiptItemDto[]? FiscalReceiptItems
);
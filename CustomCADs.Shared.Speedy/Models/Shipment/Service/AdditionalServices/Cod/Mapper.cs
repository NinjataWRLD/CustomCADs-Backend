using CustomCADs.Shared.Speedy.API.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentCodAdditionalService;

namespace CustomCADs.Shared.Speedy.Models.Shipment.Service.AdditionalServices.Cod;

public static class Mapper
{
    public static ShipmentCodAdditionalServiceDto ToDto(this ShipmentCodAdditionalServiceModel model)
        => new(
            Amount: model.Amount,
            CurrencyCode: model.CurrencyCode,
            PayoutToThirdParty: model.PayoutToThirdParty,
            PayoutToLoggedClient: model.PayoutToLoggedClient,
            IncludeShippingPrice: model.IncludeShippingPrice,
            CardPaymentForbidden: model.CardPaymentForbidden,
            ProcessingType: model.ProcessingType,
            FiscalReceiptItems: [.. model.FiscalReceiptItems?.Select(i => i.ToDto())]
        );

    public static ShipmentCodFiscalReceiptItemDto ToDto(this ShipmentCodFiscalReceiptItemModel model)
        => new(
            Description: model.Description,
            VatGroup: model.VatGroup,
            Amount: model.Amount,
            AmountWithVat: model.AmountWithVat
        );
}

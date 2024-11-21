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

    public static ShipmentCodAdditionalServiceModel ToModel(this ShipmentCodAdditionalServiceDto dto)
        => new(
            Amount: dto.Amount,
            CurrencyCode: dto.CurrencyCode,
            PayoutToThirdParty: dto.PayoutToThirdParty,
            PayoutToLoggedClient: dto.PayoutToLoggedClient,
            IncludeShippingPrice: dto.IncludeShippingPrice,
            CardPaymentForbidden: dto.CardPaymentForbidden,
            ProcessingType: dto.ProcessingType,
            FiscalReceiptItems: [.. dto.FiscalReceiptItems?.Select(i => i.ToModel())]
        );

    public static ShipmentCodFiscalReceiptItemModel ToModel(this ShipmentCodFiscalReceiptItemDto dto)
        => new(
            Description: dto.Description,
            VatGroup: dto.VatGroup,
            Amount: dto.Amount,
            AmountWithVat: dto.AmountWithVat
        );
}

namespace CustomCADs.Shared.Speedy.Models.Shipment.Service.AdditionalServices.Cod;

public record ShipmentCodFiscalReceiptItemModel(
    string Description,
    string VatGroup,
    double Amount,
    double AmountWithVat
);
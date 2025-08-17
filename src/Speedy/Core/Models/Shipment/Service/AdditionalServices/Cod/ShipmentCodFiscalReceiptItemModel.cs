namespace CustomCADs.Speedy.Core.Models.Shipment.Service.AdditionalServices.Cod;

public record ShipmentCodFiscalReceiptItemModel(
	string Description,
	string VatGroup,
	double Amount,
	double AmountWithVat
);

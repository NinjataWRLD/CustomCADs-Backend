namespace CustomCADs.Speedy.Core.Services.Models.Shipment.Service.AdditionalServices.Cod;

public record ShipmentCodFiscalReceiptItemModel(
	string Description,
	string VatGroup,
	double Amount,
	double AmountWithVat
);

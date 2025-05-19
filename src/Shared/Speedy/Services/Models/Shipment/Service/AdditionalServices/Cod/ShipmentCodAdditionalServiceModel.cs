namespace CustomCADs.Shared.Speedy.Services.Models.Shipment.Service.AdditionalServices.Cod;

public record ShipmentCodAdditionalServiceModel(
	double Amount,
	string CurrencyCode,
	bool? PayoutToThirdParty,
	bool? PayoutToLoggedClient,
	bool? IncludeShippingPrice,
	bool? CardPaymentForbidden,
	ProcessingType? ProcessingType,
	ShipmentCodFiscalReceiptItemModel[]? FiscalReceiptItems
);

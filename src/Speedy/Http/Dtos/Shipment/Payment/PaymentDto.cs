namespace CustomCADs.Speedy.Http.Dtos.Shipment.Payment;

using ShipmentPayment;

internal record PaymentDto(
	Payer CourierServicePayer,
	Payer DeclaredValuePayer,
	Payer PackagePayer,
	long ThirdPartyClientId,
	ShipmentDiscountCardIdDto DiscountCardId,
	CodPaymentDto CodPayment
);

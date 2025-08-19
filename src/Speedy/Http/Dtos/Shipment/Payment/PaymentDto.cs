namespace CustomCADs.Speedy.Http.Dtos.Shipment.Payment;

using ShipmentPayment;

/// <summary>
/// DTO representing payment details for a shipment.
/// </summary>
/// <param name="CourierServicePayer">Payer for courier service.</param>
/// <param name="DeclaredValuePayer">Payer for declared value.</param>
/// <param name="PackagePayer">Payer for package.</param>
/// <param name="ThirdPartyClientId">Third party client identifier.</param>
/// <param name="DiscountCardId">Discount card identifier.</param>
/// <param name="CodPayment">COD payment details.</param>
internal record PaymentDto(
	Payer CourierServicePayer,
	Payer DeclaredValuePayer,
	Payer PackagePayer,
	long ThirdPartyClientId,
	ShipmentDiscountCardIdDto DiscountCardId,
	CodPaymentDto CodPayment
);

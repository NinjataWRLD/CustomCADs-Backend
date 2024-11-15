namespace CustomCADs.Shared.Speedy.Dtos.Shipment.Payment;

using Enums;
using ShipmentPayment;

public record PaymentDto(
    Payer CourierServicePayer,
    Payer DeclaredValuePayer,
    Payer PackagePayer,
    long ThirdPartyClientId,
    ShipmentDiscountCardIdDto DiscountCardId,
    CodPaymentDto CodPayment
);

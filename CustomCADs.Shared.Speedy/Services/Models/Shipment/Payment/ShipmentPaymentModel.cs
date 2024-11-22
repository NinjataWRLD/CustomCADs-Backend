using CustomCADs.Shared.Speedy.Services.Models.Shipment.Payment;

namespace CustomCADs.Shared.Speedy.Services.Models.Shipment.Payment;

public record ShipmentPaymentModel(
    Payer CourierServicePayer,
    Payer? DeclaredValuePayer,
    Payer? PackagePayer,
    long? ThirdPartyClientId,
    ShipmentDiscountCardIdModel? DiscountCardId,
    BankAccountModel? SenderBankAccount,
    bool? AdministrativeFee
);
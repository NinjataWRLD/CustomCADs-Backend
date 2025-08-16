namespace CustomCADs.Speedy.Core.Services.Models.Shipment.Payment;

public record ShipmentPaymentModel(
	Payer CourierServicePayer,
	Payer? DeclaredValuePayer,
	Payer? PackagePayer,
	long? ThirdPartyClientId,
	ShipmentDiscountCardIdModel? DiscountCardId,
	BankAccountModel? SenderBankAccount,
	bool? AdministrativeFee
);

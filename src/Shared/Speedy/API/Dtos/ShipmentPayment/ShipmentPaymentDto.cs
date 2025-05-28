namespace CustomCADs.Shared.Speedy.API.Dtos.ShipmentPayment;

public record ShipmentPaymentDto(
	Payer CourierServicePayer,
	Payer? DeclaredValuePayer,
	Payer? PackagePayer,
	long? ThirdPartyClientId,
	ShipmentDiscountCardIdDto? DiscountCardId,
	BankAccountDto? SenderBankAccount,
	bool? AdministrativeFee
);

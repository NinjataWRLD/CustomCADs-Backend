namespace CustomCADs.Speedy.Http.Dtos.ShipmentPayment;

internal record ShipmentPaymentDto(
	Payer CourierServicePayer,
	Payer? DeclaredValuePayer,
	Payer? PackagePayer,
	long? ThirdPartyClientId,
	ShipmentDiscountCardIdDto? DiscountCardId,
	BankAccountDto? SenderBankAccount,
	bool? AdministrativeFee
);

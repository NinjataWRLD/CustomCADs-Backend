namespace CustomCADs.Speedy.Http.Dtos.ShipmentPayment;

internal record BankAccountDto(
	string Iban,
	string AccountHolder
);

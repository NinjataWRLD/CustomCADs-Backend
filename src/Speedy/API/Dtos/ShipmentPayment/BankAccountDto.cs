namespace CustomCADs.Speedy.API.Dtos.ShipmentPayment;

public record BankAccountDto(
	string Iban,
	string AccountHolder
);

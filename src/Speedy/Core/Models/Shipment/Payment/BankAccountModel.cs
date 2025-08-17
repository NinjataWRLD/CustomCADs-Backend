namespace CustomCADs.Speedy.Core.Models.Shipment.Payment;

public record BankAccountModel(
	string Iban,
	string AccountHolder
);

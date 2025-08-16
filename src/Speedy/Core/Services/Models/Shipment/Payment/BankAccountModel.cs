namespace CustomCADs.Speedy.Core.Services.Models.Shipment.Payment;

public record BankAccountModel(
	string Iban,
	string AccountHolder
);

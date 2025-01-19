namespace CustomCADs.Shared.Speedy.Services.Models.Shipment.Payment;

public record BankAccountModel(
    string Iban,
    string AccountHolder
);
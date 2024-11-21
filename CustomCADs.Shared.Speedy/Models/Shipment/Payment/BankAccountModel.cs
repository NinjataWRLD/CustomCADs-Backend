namespace CustomCADs.Shared.Speedy.Models.Shipment.Payment;

public record BankAccountModel(
    string Iban, 
    string AccountHolder
);
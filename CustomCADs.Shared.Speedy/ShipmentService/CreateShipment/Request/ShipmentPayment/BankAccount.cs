namespace CustomCADs.Shared.Speedy.ShipmentService.CreateShipment.Request.ShipmentPayment;

public record BankAccount(
    string Iban,
    string AccountHolder
);

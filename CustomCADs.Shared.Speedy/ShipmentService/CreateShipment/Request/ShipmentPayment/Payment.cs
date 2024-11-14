namespace CustomCADs.Shared.Speedy.ShipmentService.CreateShipment.Request.ShipmentPayment;

public record Payment(
    Payer CourierServicePayer,
    Payer? DeclaredValuePayer,
    Payer? PackagePayer,
    BankAccount? SenderBankAccount
);

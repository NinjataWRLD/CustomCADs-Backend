namespace CustomCADs.Shared.Speedy.ShipmentService.Dtos;

public record Payment(
    Payer CourierServicePayer,
    Payer? DeclaredValuePayer,
    Payer? PackagePayer,
    BankAccount? SenderBankAccount
);

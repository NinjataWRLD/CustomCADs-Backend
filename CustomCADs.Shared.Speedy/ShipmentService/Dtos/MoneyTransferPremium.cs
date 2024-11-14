namespace CustomCADs.Shared.Speedy.ShipmentService.Dtos;

public record MoneyTransferPremium(
    double Amount,
    double AmountLocal,
    Payer Payer
);

namespace CustomCADs.Shared.Speedy.Dtos.ShipmentPrice;

public record MoneyTransferPremiumDto(
    double? Amount,
    double? AmountLocal,
    Payer? Payer
);

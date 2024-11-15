namespace CustomCADs.Shared.Speedy.Dtos.ShipmentPrice;

using Enums;

public record MoneyTransferPremiumDto(
    double? Amount,
    double? AmountLocal,
    Payer? Payer
);

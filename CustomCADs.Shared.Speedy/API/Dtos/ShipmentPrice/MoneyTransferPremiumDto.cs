namespace CustomCADs.Shared.Speedy.API.Dtos.ShipmentPrice;

using Enums;

public record MoneyTransferPremiumDto(
    double? Amount,
    double? AmountLocal,
    Payer? Payer
);

namespace CustomCADs.Shared.Speedy.Services.Shipment.Models;

public record MoneyTransferPremiumModel(
    double? Amount,
    double? AmountLocal,
    Payer? Payer
);
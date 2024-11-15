namespace CustomCADs.Shared.Speedy.Dtos.ShipmentPrice;

public record ShipmentPriceAmountDto(
    double Amount,
    double VatPercent,
    double? Percent
);

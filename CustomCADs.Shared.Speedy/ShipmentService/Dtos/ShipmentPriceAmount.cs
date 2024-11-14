namespace CustomCADs.Shared.Speedy.ShipmentService.Dtos;
public record ShipmentPriceAmount(
    double Amount,
    double Percent,
    double VatPercent
);

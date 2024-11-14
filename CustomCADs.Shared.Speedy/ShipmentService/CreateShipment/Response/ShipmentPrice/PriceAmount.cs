namespace CustomCADs.Shared.Speedy.ShipmentService.CreateShipment.Response.ShipmentPrice;
public record PriceAmount(
    double Amount,
    double Percent,
    double VatPercent
);

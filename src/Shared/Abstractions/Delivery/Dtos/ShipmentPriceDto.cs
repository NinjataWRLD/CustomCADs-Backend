namespace CustomCADs.Shared.Abstractions.Delivery.Dtos;

public record ShipmentPriceDto(
    double Amount,
    double Vat,
    double Total,
    string Currency
);
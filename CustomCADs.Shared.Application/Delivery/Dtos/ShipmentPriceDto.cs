namespace CustomCADs.Shared.Application.Delivery.Dtos;

public record ShipmentPriceDto(
    double Amount,
    double Vat,
    double Total,
    string Currency
);
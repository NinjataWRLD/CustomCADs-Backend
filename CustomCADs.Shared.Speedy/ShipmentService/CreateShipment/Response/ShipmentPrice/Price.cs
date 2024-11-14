namespace CustomCADs.Shared.Speedy.ShipmentService.CreateShipment.Response.ShipmentPrice;

public record Price(
    double Amount,
    double Vat,
    double Total,
    string Currency,
    Dictionary<string, PriceAmount> Details,
    double AmountLocal,
    double VatLocal,
    double TotalLocal,
    string CurrencyLocal,
    Dictionary<string, PriceAmount> DetailsLocal,
    int CurrencyExchangeRateUnit,
    double CurrencyExchangeRate,
    ReturnAmounts ReturnAmounts
);

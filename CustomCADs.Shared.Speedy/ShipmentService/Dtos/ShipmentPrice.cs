namespace CustomCADs.Shared.Speedy.ShipmentService.Dtos;

public record ShipmentPrice(
    double Amount,
    double Vat,
    double Total,
    string Currency,
    Dictionary<string, ShipmentPriceAmount> Details,
    double AmountLocal,
    double VatLocal,
    double TotalLocal,
    string CurrencyLocal,
    Dictionary<string, ShipmentPriceAmount> DetailsLocal,
    int CurrencyExchangeRateUnit,
    double CurrencyExchangeRate,
    ReturnAmounts ReturnAmounts
);

namespace CustomCADs.Shared.Speedy.Services.LocationService.Country.FindCountry;

public record FindCountryRequest(
    string UserName,
    string Password,
    string? Location,
    long? ClientSystemId,
    string? Name,
    string? IsoAlpha2,
    string? IsoAlpha3
);
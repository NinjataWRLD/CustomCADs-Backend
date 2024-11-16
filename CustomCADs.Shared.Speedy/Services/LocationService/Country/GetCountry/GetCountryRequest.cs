namespace CustomCADs.Shared.Speedy.Services.LocationService.Country.GetCountry;

public record GetCountryRequest(
    string UserName,
    string Password,
    string? Location,
    long? ClientSystemId
);
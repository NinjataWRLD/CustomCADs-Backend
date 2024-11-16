namespace CustomCADs.Shared.Speedy.Services.LocationService.Country.GetAllCountries;

public record GetAllCountriesRequest(
    string UserName,
    string Password,
    string? Location,
    long? ClientSystemId
);
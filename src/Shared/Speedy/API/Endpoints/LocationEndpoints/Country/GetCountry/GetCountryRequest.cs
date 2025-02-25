namespace CustomCADs.Shared.Speedy.API.Endpoints.LocationEndpoints.Country.GetCountry;

public record GetCountryRequest(
    string UserName,
    string Password,
    string? Language,
    long? ClientSystemId
);
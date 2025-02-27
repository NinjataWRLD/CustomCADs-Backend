namespace CustomCADs.Shared.Speedy.API.Endpoints.LocationEndpoints.Country.FindCountry;

using Dtos.Country;

public record FindCountryResponse(
    CountryDto[]? Countries,
    ErrorDto? Error
);
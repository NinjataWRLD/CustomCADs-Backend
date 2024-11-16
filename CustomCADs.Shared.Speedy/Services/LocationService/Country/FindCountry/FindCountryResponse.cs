namespace CustomCADs.Shared.Speedy.Services.LocationService.Country.FindCountry;

using Dtos.Country;

public record FindCountryResponse(
    CountryDto[]? Countries,
    ErrorDto? Error
);
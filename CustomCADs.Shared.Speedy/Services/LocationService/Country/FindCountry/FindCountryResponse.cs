namespace CustomCADs.Shared.Speedy.Services.LocationService.Country.FindCountry;

using Dtos.Country;
using Dtos.Errors;

public record FindCountryResponse(
    CountryDto[]? Countries,
    ErrorDto? Error
);
namespace CustomCADs.Shared.Speedy.Services.LocationService.Country.GetCountry;

using Dtos.Country;

public record GetCountryResponse(
    CountryDto? Country,
    ErrorDto? Error
);
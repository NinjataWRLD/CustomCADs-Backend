namespace CustomCADs.Shared.Speedy.Services.LocationService.Country.GetCountry;

using Dtos.Country;
using Dtos.Errors;

public record GetCountryResponse(
    CountryDto? Country,
    ErrorDto? Error
);
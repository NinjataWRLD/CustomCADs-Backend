namespace CustomCADs.Shared.Speedy.API.Endpoints.LocationEndpoints.Country.GetCountry;

using Dtos.Country;

public record GetCountryResponse(
	CountryDto? Country,
	ErrorDto? Error
);

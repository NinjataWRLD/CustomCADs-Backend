namespace CustomCADs.Speedy.Http.Endpoints.LocationEndpoints.Country.FindCountry;

using Dtos.Country;

internal record FindCountryResponse(
	CountryDto[]? Countries,
	ErrorDto? Error
);

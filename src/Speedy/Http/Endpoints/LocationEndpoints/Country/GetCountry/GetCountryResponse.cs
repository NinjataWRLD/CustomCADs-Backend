namespace CustomCADs.Speedy.Http.Endpoints.LocationEndpoints.Country.GetCountry;

using Dtos.Country;

internal record GetCountryResponse(
	CountryDto? Country,
	ErrorDto? Error
);

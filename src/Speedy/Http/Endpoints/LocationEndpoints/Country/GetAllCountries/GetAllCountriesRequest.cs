namespace CustomCADs.Speedy.Http.Endpoints.LocationEndpoints.Country.GetAllCountries;

internal record GetAllCountriesRequest(
	string UserName,
	string Password,
	string? Language,
	long? ClientSystemId
);

namespace CustomCADs.Speedy.Http.Endpoints.LocationEndpoints.Country.GetCountry;

internal record GetCountryRequest(
	string UserName,
	string Password,
	string? Language,
	long? ClientSystemId
);

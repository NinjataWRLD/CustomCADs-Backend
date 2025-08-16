namespace CustomCADs.Speedy.Http.Endpoints.LocationEndpoints.Country.FindCountry;

internal record FindCountryRequest(
	string UserName,
	string Password,
	string? Language,
	long? ClientSystemId,
	string? Name,
	string? IsoAlpha2,
	string? IsoAlpha3
);

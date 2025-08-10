namespace CustomCADs.Speedy.API.Endpoints.LocationEndpoints.Country.GetAllCountries;

public record GetAllCountriesRequest(
	string UserName,
	string Password,
	string? Language,
	long? ClientSystemId
);

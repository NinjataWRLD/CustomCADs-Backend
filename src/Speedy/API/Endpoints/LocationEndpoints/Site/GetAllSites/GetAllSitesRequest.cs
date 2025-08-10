namespace CustomCADs.Speedy.API.Endpoints.LocationEndpoints.Site.GetAllSites;

public record GetAllSitesRequest(
	string UserName,
	string Password,
	string? Language,
	long? ClientSystemId
);

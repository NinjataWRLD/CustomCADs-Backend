namespace CustomCADs.Speedy.Http.Endpoints.LocationEndpoints.Site.GetAllSites;

internal record GetAllSitesRequest(
	string UserName,
	string Password,
	string? Language,
	long? ClientSystemId
);

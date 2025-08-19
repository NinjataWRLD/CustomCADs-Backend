namespace CustomCADs.Speedy.Http.Endpoints.LocationEndpoints.Site.GetSite;

internal record GetSiteRequest(
	string UserName,
	string Password,
	string? Language,
	long? ClientSystemId
);

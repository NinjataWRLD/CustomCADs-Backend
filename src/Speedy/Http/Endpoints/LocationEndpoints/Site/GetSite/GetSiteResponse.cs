namespace CustomCADs.Speedy.Http.Endpoints.LocationEndpoints.Site.GetSite;

using Dtos.Site;

internal record GetSiteResponse(
	SiteDto? Site,
	ErrorDto? Error
);

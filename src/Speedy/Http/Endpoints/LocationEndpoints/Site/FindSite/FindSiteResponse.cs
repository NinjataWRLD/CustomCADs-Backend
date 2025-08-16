namespace CustomCADs.Speedy.Http.Endpoints.LocationEndpoints.Site.FindSite;

using Dtos.Site;

internal record FindSiteResponse(
	SiteDto[]? Sites,
	ErrorDto? Error
);

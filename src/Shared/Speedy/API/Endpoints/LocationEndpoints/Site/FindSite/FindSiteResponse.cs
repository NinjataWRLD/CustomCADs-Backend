namespace CustomCADs.Shared.Speedy.API.Endpoints.LocationEndpoints.Site.FindSite;

using Dtos.Site;

public record FindSiteResponse(
	SiteDto[]? Sites,
	ErrorDto? Error
);

using CustomCADs.Shared.Speedy.Dtos.Errors;
using CustomCADs.Shared.Speedy.Dtos.Site;

namespace CustomCADs.Shared.Speedy.Services.LocationService.Site.FindSite;

public record FindSiteResponse(
    SiteDto[]? Sites,
    ErrorDto? Error
);
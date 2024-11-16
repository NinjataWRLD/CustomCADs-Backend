namespace CustomCADs.Shared.Speedy.API.Endpoints.LocationEndpoints.Site.GetAllSites;

public record GetAllSitesRequest(
    string UserName,
    string Password,
    string? Location,
    long? ClientSystemId
);
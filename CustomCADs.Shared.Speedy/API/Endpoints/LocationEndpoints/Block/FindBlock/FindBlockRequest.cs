namespace CustomCADs.Shared.Speedy.API.Endpoints.LocationEndpoints.Block.FindBlock;

public record FindBlockRequest(
    string UserName,
    string Password,
    int SiteId,
    string? Location,
    long? ClientSystemId,
    string? Name,
    string? Type
);
namespace CustomCADs.Shared.Speedy.API.Endpoints.LocationEndpoints.Block.FindBlock;

public record FindBlockRequest(
	string UserName,
	string Password,
	int SiteId,
	string? Language,
	long? ClientSystemId,
	string? Name,
	string? Type
);

namespace CustomCADs.Speedy.API.Endpoints.LocationEndpoints.Block.GetAllBlocks;

public record GetAllBlocksRequest(
	string UserName,
	string Password,
	string? Language,
	long? ClientSystemId
);

namespace CustomCADs.Speedy.Http.Endpoints.LocationEndpoints.Block.GetAllBlocks;

internal record GetAllBlocksRequest(
	string UserName,
	string Password,
	string? Language,
	long? ClientSystemId
);

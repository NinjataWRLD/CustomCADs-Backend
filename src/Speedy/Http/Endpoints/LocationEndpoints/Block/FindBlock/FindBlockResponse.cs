namespace CustomCADs.Speedy.Http.Endpoints.LocationEndpoints.Block.FindBlock;

using Dtos.Block;

internal record FindBlockResponse(
	BlockDto[]? Blocks,
	ErrorDto? Error
);

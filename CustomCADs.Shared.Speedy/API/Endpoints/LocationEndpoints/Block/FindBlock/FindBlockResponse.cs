namespace CustomCADs.Shared.Speedy.API.Endpoints.LocationEndpoints.Block.FindBlock;

using Dtos.Block;

public record FindBlockResponse(
    BlockDto[]? Blocks,
    ErrorDto? Error
);
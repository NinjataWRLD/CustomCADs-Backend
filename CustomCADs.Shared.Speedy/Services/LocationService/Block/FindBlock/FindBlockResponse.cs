namespace CustomCADs.Shared.Speedy.Services.LocationService.Block.FindBlock;

using Dtos.Block;

public record FindBlockResponse(
    BlockDto[]? Blocks,
    ErrorDto? Error
);
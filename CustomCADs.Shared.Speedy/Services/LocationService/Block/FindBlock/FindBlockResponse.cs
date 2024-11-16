namespace CustomCADs.Shared.Speedy.Services.LocationService.Block.FindBlock;

using Dtos.Block;
using Dtos.Errors;

public record FindBlockResponse(
    BlockDto[]? Blocks,
    ErrorDto? Error
);
namespace CustomCADs.Shared.Speedy.Services.LocationService.Block.GetAllBlocks;

public record GetAllBlocksRequest(
    string UserName,
    string Password,
    string? Location,
    long? ClientSystemId
);
using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.Catalog.Application.Common.Dtos;

public record ImageDto(
    ImageId Id,
    string Key,
    string ContentType
);

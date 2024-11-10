using CustomCADs.Shared.Core.Domain.ValueObjects;

namespace CustomCADs.Shared.Core.Dtos;

public record ImageDto(string Path)
{
    public ImageDto(Image image) : this(image.Path)
    { }
}

namespace CustomCADs.Shared.Core.Domain.ValueObjects;

public record Image(string Path)
{
    public Image() : this(string.Empty) { }
}

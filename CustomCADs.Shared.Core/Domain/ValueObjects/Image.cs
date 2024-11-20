namespace CustomCADs.Shared.Core.Domain.ValueObjects;

public record Image(string Key, string ContentType)
{
    public Image() : this(string.Empty, string.Empty) { }
}

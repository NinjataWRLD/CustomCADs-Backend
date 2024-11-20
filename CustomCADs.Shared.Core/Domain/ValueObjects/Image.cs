namespace CustomCADs.Shared.Core.Domain.ValueObjects;

public record Image(string Key)
{
    public Image() : this(string.Empty) { }
}

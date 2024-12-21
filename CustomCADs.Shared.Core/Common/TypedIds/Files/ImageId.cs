using System.Diagnostics.CodeAnalysis;

namespace CustomCADs.Shared.Core.Common.TypedIds.Files;

public readonly struct ImageId(Guid value)
{
    public ImageId() : this(Guid.Empty) { }

    public Guid Value { get; } = value;

    public override bool Equals([NotNullWhen(true)] object? obj)
        => obj is ImageId imageId && this == imageId;

    public override int GetHashCode()
        => Value.GetHashCode();

    public override string ToString()
        => Value.ToString();

    public static bool operator ==(ImageId left, ImageId right)
        => left.Value == right.Value;

    public static bool operator !=(ImageId left, ImageId right)
        => !(left == right);
}

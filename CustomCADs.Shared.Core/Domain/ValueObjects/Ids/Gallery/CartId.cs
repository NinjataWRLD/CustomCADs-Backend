using System.Diagnostics.CodeAnalysis;

namespace CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Gallery;

public readonly struct CartId(Guid value)
{
    public CartId() : this(Guid.Empty) { }
    public Guid Value { get; init; } = value;

    public override bool Equals([NotNullWhen(true)] object? obj)
        => obj is CartId cartId && this == cartId;

    public override int GetHashCode()
        => Value.GetHashCode();

    public override string ToString()
        => Value.ToString();

    public static bool operator ==(CartId left, CartId right)
        => left.Value == right.Value;

    public static bool operator !=(CartId left, CartId right)
        => !(left == right);
}

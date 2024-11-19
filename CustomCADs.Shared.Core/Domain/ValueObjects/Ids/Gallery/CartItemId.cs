using System.Diagnostics.CodeAnalysis;

namespace CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Gallery;

public readonly struct CartItemId(Guid value)
{
    public CartItemId() : this(Guid.Empty) { }
    public Guid Value { get; init; } = value;

    public override bool Equals([NotNullWhen(true)] object? obj)
        => obj is CartItemId orderId && this == orderId;

    public override int GetHashCode()
        => Value.GetHashCode();

    public override string ToString()
        => Value.ToString();

    public static bool operator ==(CartItemId left, CartItemId right)
        => left.Value == right.Value;

    public static bool operator !=(CartItemId left, CartItemId right)
        => !(left == right);
}

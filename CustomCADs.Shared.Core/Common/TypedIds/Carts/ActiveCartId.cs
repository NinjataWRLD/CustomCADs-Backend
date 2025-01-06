using System.Diagnostics.CodeAnalysis;

namespace CustomCADs.Shared.Core.Common.TypedIds.Carts;

public readonly struct ActiveCartId(Guid value)
{
    public ActiveCartId() : this(Guid.Empty) { }
    public Guid Value { get; init; } = value;

    public override bool Equals([NotNullWhen(true)] object? obj)
        => obj is ActiveCartId cartId && this == cartId;

    public override int GetHashCode()
        => Value.GetHashCode();

    public override string ToString()
        => Value.ToString();

    public static bool operator ==(ActiveCartId left, ActiveCartId right)
        => left.Value == right.Value;

    public static bool operator !=(ActiveCartId left, ActiveCartId right)
        => !(left == right);
}

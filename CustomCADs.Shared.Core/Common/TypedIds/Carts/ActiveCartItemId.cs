using System.Diagnostics.CodeAnalysis;

namespace CustomCADs.Shared.Core.Common.TypedIds.Carts;

public readonly struct ActiveCartItemId(Guid value)
{
    public ActiveCartItemId() : this(Guid.Empty) { }
    public Guid Value { get; init; } = value;

    public override bool Equals([NotNullWhen(true)] object? obj)
        => obj is ActiveCartItemId orderId && this == orderId;

    public override int GetHashCode()
        => Value.GetHashCode();

    public override string ToString()
        => Value.ToString();

    public static bool operator ==(ActiveCartItemId left, ActiveCartItemId right)
        => left.Value == right.Value;

    public static bool operator !=(ActiveCartItemId left, ActiveCartItemId right)
        => !(left == right);
}

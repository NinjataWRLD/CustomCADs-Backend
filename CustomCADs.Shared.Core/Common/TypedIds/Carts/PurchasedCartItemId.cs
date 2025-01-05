using System.Diagnostics.CodeAnalysis;

namespace CustomCADs.Shared.Core.Common.TypedIds.Carts;

public readonly struct PurchasedCartItemId(Guid value)
{
    public PurchasedCartItemId() : this(Guid.Empty) { }
    public Guid Value { get; init; } = value;

    public override bool Equals([NotNullWhen(true)] object? obj)
        => obj is PurchasedCartItemId orderId && this == orderId;

    public override int GetHashCode()
        => Value.GetHashCode();

    public override string ToString()
        => Value.ToString();

    public static bool operator ==(PurchasedCartItemId left, PurchasedCartItemId right)
        => left.Value == right.Value;

    public static bool operator !=(PurchasedCartItemId left, PurchasedCartItemId right)
        => !(left == right);
}

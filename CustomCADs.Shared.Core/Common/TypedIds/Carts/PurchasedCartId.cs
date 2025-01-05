using System.Diagnostics.CodeAnalysis;

namespace CustomCADs.Shared.Core.Common.TypedIds.Carts;

public readonly struct PurchasedCartId(Guid value)
{
    public PurchasedCartId() : this(Guid.Empty) { }
    public Guid Value { get; init; } = value;

    public override bool Equals([NotNullWhen(true)] object? obj)
        => obj is PurchasedCartId cartId && this == cartId;

    public override int GetHashCode()
        => Value.GetHashCode();

    public override string ToString()
        => Value.ToString();

    public static bool operator ==(PurchasedCartId left, PurchasedCartId right)
        => left.Value == right.Value;

    public static bool operator !=(PurchasedCartId left, PurchasedCartId right)
        => !(left == right);
}

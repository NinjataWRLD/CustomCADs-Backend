using System.Diagnostics.CodeAnalysis;

namespace CustomCADs.Shared.Core.Common.TypedIds.Carts;

public readonly struct PurchasedCartId
{
    public PurchasedCartId() : this(Guid.Empty) { }
    private PurchasedCartId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; init; }

    public static PurchasedCartId New() => new(Guid.NewGuid());
    public static PurchasedCartId New(Guid id) => new(id);

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

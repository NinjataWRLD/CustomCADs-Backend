using System.Diagnostics.CodeAnalysis;

namespace CustomCADs.Shared.Core.Common.TypedIds.Carts;

public readonly struct PurchasedCartItemId
{
    public PurchasedCartItemId() : this(Guid.Empty) { }
    private PurchasedCartItemId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; init; }
    
    public static PurchasedCartItemId New() => new(Guid.NewGuid());
    public static PurchasedCartItemId New(Guid id) => new(id);

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

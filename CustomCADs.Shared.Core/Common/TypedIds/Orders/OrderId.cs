using System.Diagnostics.CodeAnalysis;

namespace CustomCADs.Shared.Core.Common.TypedIds.Orders;

public readonly struct OrderId
{
    public OrderId() : this(Guid.Empty) { }
    private OrderId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; init; }

    public static OrderId New() => new(Guid.NewGuid());
    public static OrderId New(Guid id) => new(id);

    public override bool Equals([NotNullWhen(true)] object? obj)
        => obj is OrderId orderId && this == orderId;

    public override int GetHashCode()
        => Value.GetHashCode();

    public override string ToString()
        => Value.ToString();

    public static bool operator ==(OrderId left, OrderId right)
        => left.Value == right.Value;

    public static bool operator !=(OrderId left, OrderId right)
        => !(left == right);
}

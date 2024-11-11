using System.Diagnostics.CodeAnalysis;

namespace CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Orders;

public readonly struct OrderId(Guid value)
{
    public OrderId() : this(Guid.Empty) { }
    public Guid Value { get; init; } = value;

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

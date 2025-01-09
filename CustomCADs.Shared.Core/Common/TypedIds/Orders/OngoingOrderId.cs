using System.Diagnostics.CodeAnalysis;

namespace CustomCADs.Shared.Core.Common.TypedIds.Orders;

public readonly struct OngoingOrderId
{
    public OngoingOrderId() : this(Guid.Empty) { }
    private OngoingOrderId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; init; }

    public static OngoingOrderId New() => new(Guid.NewGuid());
    public static OngoingOrderId New(Guid id) => new(id);

    public override bool Equals([NotNullWhen(true)] object? obj)
        => obj is OngoingOrderId orderId && this == orderId;

    public override int GetHashCode()
        => Value.GetHashCode();

    public override string ToString()
        => Value.ToString();

    public static bool operator ==(OngoingOrderId left, OngoingOrderId right)
        => left.Value == right.Value;

    public static bool operator !=(OngoingOrderId left, OngoingOrderId right)
        => !(left == right);
}

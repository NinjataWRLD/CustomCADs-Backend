using System.Diagnostics.CodeAnalysis;

namespace CustomCADs.Shared.Core.Common.TypedIds.Orders;

public readonly struct CompletedOrderId
{
    public CompletedOrderId() : this(Guid.Empty) { }
    private CompletedOrderId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; init; }

    public static CompletedOrderId New() => new(Guid.NewGuid());
    public static CompletedOrderId New(Guid id) => new(id);

    public override bool Equals([NotNullWhen(true)] object? obj)
        => obj is CompletedOrderId orderId && this == orderId;

    public override int GetHashCode()
        => Value.GetHashCode();

    public override string ToString()
        => Value.ToString();

    public static bool operator ==(CompletedOrderId left, CompletedOrderId right)
        => left.Value == right.Value;

    public static bool operator !=(CompletedOrderId left, CompletedOrderId right)
        => !(left == right);
}

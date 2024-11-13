using System.Diagnostics.CodeAnalysis;

namespace CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Orders;

public readonly struct CustomOrderId(Guid value)
{
    public CustomOrderId() : this(Guid.Empty) { }
    public Guid Value { get; init; } = value;

    public override bool Equals([NotNullWhen(true)] object? obj)
        => obj is CustomOrderId orderId && this == orderId;

    public override int GetHashCode()
        => Value.GetHashCode();

    public override string ToString()
        => Value.ToString();

    public static bool operator ==(CustomOrderId left, CustomOrderId right)
        => left.Value == right.Value;

    public static bool operator !=(CustomOrderId left, CustomOrderId right)
        => !(left == right);
}

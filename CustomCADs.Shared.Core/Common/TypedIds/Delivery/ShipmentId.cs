using System.Diagnostics.CodeAnalysis;

namespace CustomCADs.Shared.Core.Common.TypedIds.Delivery;

public readonly struct ShipmentId(Guid value)
{
    public ShipmentId() : this(Guid.Empty) { }

    public Guid Value { get; } = value;

    public override bool Equals([NotNullWhen(true)] object? obj)
        => obj is ShipmentId deliveryId && this == deliveryId;

    public override int GetHashCode()
        => Value.GetHashCode();

    public override string ToString()
        => Value.ToString();

    public static bool operator ==(ShipmentId left, ShipmentId right)
        => left.Value == right.Value;

    public static bool operator !=(ShipmentId left, ShipmentId right)
        => !(left == right);
}

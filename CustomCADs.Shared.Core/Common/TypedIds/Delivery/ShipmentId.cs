using System.Diagnostics.CodeAnalysis;

namespace CustomCADs.Shared.Core.Common.TypedIds.Delivery;

public readonly struct ShipmentId
{
    public ShipmentId() : this(Guid.Empty) { }
    private ShipmentId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static ShipmentId New() => new(Guid.NewGuid());
    public static ShipmentId New(Guid id) => new(id);
    public static ShipmentId? New(Guid? id) => id is null ? null : new(id.Value);

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

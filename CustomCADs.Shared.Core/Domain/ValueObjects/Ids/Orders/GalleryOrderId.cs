using System.Diagnostics.CodeAnalysis;

namespace CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Orders;

public readonly struct GalleryOrderId(Guid value)
{
    public GalleryOrderId() : this(Guid.Empty) { }
    public Guid Value { get; init; } = value;

    public override bool Equals([NotNullWhen(true)] object? obj)
        => obj is GalleryOrderId cartId && this == cartId;

    public override int GetHashCode()
        => Value.GetHashCode();

    public override string ToString()
        => Value.ToString();

    public static bool operator ==(GalleryOrderId left, GalleryOrderId right)
        => left.Value == right.Value;

    public static bool operator !=(GalleryOrderId left, GalleryOrderId right)
        => !(left == right);
}

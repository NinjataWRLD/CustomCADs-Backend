using System.Diagnostics.CodeAnalysis;

namespace CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Orders;

public readonly struct GalleryOrderItemId(Guid value)
{
    public GalleryOrderItemId() : this(Guid.Empty) { }
    public Guid Value { get; init; } = value;

    public override bool Equals([NotNullWhen(true)] object? obj)
        => obj is GalleryOrderItemId itemId && this == itemId;

    public override int GetHashCode()
        => Value.GetHashCode();

    public override string ToString()
        => Value.ToString();

    public static bool operator ==(GalleryOrderItemId left, GalleryOrderItemId right)
        => left.Value == right.Value;

    public static bool operator !=(GalleryOrderItemId left, GalleryOrderItemId right)
        => !(left == right);
}

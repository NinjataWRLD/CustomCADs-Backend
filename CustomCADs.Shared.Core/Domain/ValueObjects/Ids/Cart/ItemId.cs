using System.Diagnostics.CodeAnalysis;

namespace CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Cart;

public readonly struct ItemId(Guid value)
{
    public ItemId() : this(Guid.Empty) { }
    public Guid Value { get; init; } = value;

    public override bool Equals([NotNullWhen(true)] object? obj)
        => obj is ItemId itemId && this == itemId;

    public override int GetHashCode()
        => Value.GetHashCode();

    public override string ToString()
        => Value.ToString();

    public static bool operator ==(ItemId left, ItemId right)
        => left.Value == right.Value;

    public static bool operator !=(ItemId left, ItemId right)
        => !(left == right);
}

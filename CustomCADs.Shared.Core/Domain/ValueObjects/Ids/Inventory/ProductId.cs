using System.Diagnostics.CodeAnalysis;

namespace CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Inventory;

public readonly struct ProductId(Guid value)
{
    public ProductId() : this(Guid.Empty) { }
    public Guid Value { get; init; } = value;

    public override bool Equals([NotNullWhen(true)] object? obj)
        => obj is ProductId productId && this == productId;

    public override int GetHashCode()
        => Value.GetHashCode();

    public override string ToString()
        => Value.ToString();

    public static bool operator ==(ProductId left, ProductId right)
        => left.Value == right.Value;

    public static bool operator !=(ProductId left, ProductId right)
        => !(left == right);
}

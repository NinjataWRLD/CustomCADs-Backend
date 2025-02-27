using System.Diagnostics.CodeAnalysis;

namespace CustomCADs.Shared.Core.Common.TypedIds.Carts;

public readonly struct ActiveCartId
{
    public ActiveCartId() : this(Guid.Empty) { }
    private ActiveCartId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; init; }

    public static ActiveCartId New() => new(Guid.NewGuid());
    public static ActiveCartId New(Guid id) => new(id);

    public override bool Equals([NotNullWhen(true)] object? obj)
        => obj is ActiveCartId cartId && this == cartId;

    public override int GetHashCode()
        => Value.GetHashCode();

    public override string ToString()
        => Value.ToString();

    public static bool operator ==(ActiveCartId left, ActiveCartId right)
        => left.Value == right.Value;

    public static bool operator !=(ActiveCartId left, ActiveCartId right)
        => !(left == right);
}

using System.Diagnostics.CodeAnalysis;

namespace CustomCADs.Shared.Core.Common.TypedIds.Customs;

public readonly struct CustomId
{
    public CustomId() : this(Guid.Empty) { }
    private CustomId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; init; }

    public static CustomId New() => new(Guid.NewGuid());
    public static CustomId New(Guid id) => new(id);

    public override bool Equals([NotNullWhen(true)] object? obj)
        => obj is CustomId id && this == id;

    public override int GetHashCode()
        => Value.GetHashCode();

    public override string ToString()
        => Value.ToString();

    public static bool operator ==(CustomId left, CustomId right)
        => left.Value == right.Value;

    public static bool operator !=(CustomId left, CustomId right)
        => !(left == right);
}

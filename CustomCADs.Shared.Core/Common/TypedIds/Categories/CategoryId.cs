using System.Diagnostics.CodeAnalysis;

namespace CustomCADs.Shared.Core.Common.TypedIds.Categories;

public readonly struct CategoryId(int value)
{
    public CategoryId() : this(0) { }
    public int Value { get; init; } = value;

    public override bool Equals([NotNullWhen(true)] object? obj)
        => obj is CategoryId categoryId && this == categoryId;

    public override int GetHashCode()
        => Value.GetHashCode();

    public override string ToString()
        => Value.ToString();

    public static bool operator ==(CategoryId left, CategoryId right)
        => left.Value == right.Value;

    public static bool operator !=(CategoryId left, CategoryId right)
        => !(left == right);
}

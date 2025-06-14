using System.Diagnostics.CodeAnalysis;

namespace CustomCADs.Shared.Core.Common.TypedIds.Categories;

public readonly struct CategoryId
{
	public CategoryId() : this(0) { }
	private CategoryId(int value)
	{
		Value = value;
	}

	public int Value { get; init; }

	public static CategoryId New() => new(0);
	public static CategoryId New(int id) => new(id);
	public static CategoryId? New(int? id) => id is null ? null : new(id.Value);
	public static int Unwrap(CategoryId id) => id.Value;
	public static int? Unwrap(CategoryId? id) => id?.Value;

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

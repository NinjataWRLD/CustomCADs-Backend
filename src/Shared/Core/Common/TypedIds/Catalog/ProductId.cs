using System.Diagnostics.CodeAnalysis;

namespace CustomCADs.Shared.Core.Common.TypedIds.Catalog;

public readonly struct ProductId
{
	public ProductId() : this(Guid.Empty) { }
	private ProductId(Guid value)
	{
		Value = value;
	}

	public Guid Value { get; init; }

	public static ProductId New() => new(Guid.NewGuid());
	public static ProductId New(Guid id) => new(id);

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

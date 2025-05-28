using System.Diagnostics.CodeAnalysis;

namespace CustomCADs.Shared.Core.Common.TypedIds.Catalog;

public readonly struct TagId
{
	public TagId() : this(Guid.Empty) { }
	private TagId(Guid value)
	{
		Value = value;
	}

	public Guid Value { get; init; }

	public static TagId New() => new(Guid.NewGuid());
	public static TagId New(Guid id) => new(id);
	public static TagId New(string id) => new(Guid.Parse(id));
	public static TagId[]? New(Guid[]? ids) => ids is null ? null : [.. ids.Select(New)];

	public override bool Equals([NotNullWhen(true)] object? obj)
		=> obj is TagId TagId && this == TagId;

	public override int GetHashCode()
		=> Value.GetHashCode();

	public override string ToString()
		=> Value.ToString();

	public static bool operator ==(TagId left, TagId right)
		=> left.Value == right.Value;

	public static bool operator !=(TagId left, TagId right)
		=> !(left == right);
}

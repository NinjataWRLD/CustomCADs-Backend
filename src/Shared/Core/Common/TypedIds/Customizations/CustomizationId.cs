using System.Diagnostics.CodeAnalysis;

namespace CustomCADs.Shared.Core.Common.TypedIds.Customizations;

public readonly struct CustomizationId
{
	public CustomizationId() : this(Guid.Empty) { }
	private CustomizationId(Guid value)
	{
		Value = value;
	}

	public Guid Value { get; init; }

	public static CustomizationId New() => new(Guid.NewGuid());
	public static CustomizationId New(Guid id) => new(id);
	public static CustomizationId? New(Guid? id) => id is null ? null : new(id.Value);
	public static Guid Unwrap(CustomizationId id) => id.Value;
	public static Guid? Unwrap(CustomizationId? id) => id?.Value;

	public override bool Equals([NotNullWhen(true)] object? obj)
		=> obj is CustomizationId id && this == id;

	public override int GetHashCode()
		=> Value.GetHashCode();

	public override string ToString()
		=> Value.ToString();

	public static bool operator ==(CustomizationId left, CustomizationId right)
		=> left.Value == right.Value;

	public static bool operator !=(CustomizationId left, CustomizationId right)
		=> !(left == right);
}

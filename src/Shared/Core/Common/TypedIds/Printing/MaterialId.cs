using System.Diagnostics.CodeAnalysis;

namespace CustomCADs.Shared.Core.Common.TypedIds.Printing;

public readonly struct MaterialId
{
	public MaterialId() : this(0) { }
	private MaterialId(int value)
	{
		Value = value;
	}

	public int Value { get; init; }

	public static MaterialId New() => new(0);
	public static MaterialId New(int id) => new(id);
	public static int Unwrap(MaterialId id) => id.Value;
	public static int? Unwrap(MaterialId? id) => id?.Value;

	public override bool Equals([NotNullWhen(true)] object? obj)
		=> obj is MaterialId id && this == id;

	public override int GetHashCode()
		=> Value.GetHashCode();

	public override string ToString()
		=> Value.ToString();

	public static bool operator ==(MaterialId left, MaterialId right)
		=> left.Value == right.Value;

	public static bool operator !=(MaterialId left, MaterialId right)
		=> !(left == right);
}

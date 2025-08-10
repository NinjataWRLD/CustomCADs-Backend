using System.Diagnostics.CodeAnalysis;

namespace CustomCADs.Shared.Domain.TypedIds.Files;

public readonly struct CadId
{
	public CadId() : this(Guid.Empty) { }
	private CadId(Guid value)
	{
		Value = value;
	}

	public Guid Value { get; }

	public static CadId New() => new(Guid.NewGuid());
	public static CadId New(Guid id) => new(id);
	public static CadId? New(Guid? id) => id is null ? null : new(id.Value);
	public static Guid Unwrap(CadId id) => id.Value;
	public static Guid? Unwrap(CadId? id) => id?.Value;

	public override bool Equals([NotNullWhen(true)] object? obj)
		=> obj is CadId cadId && this == cadId;

	public override int GetHashCode()
		=> Value.GetHashCode();

	public override string ToString()
		=> Value.ToString();

	public static bool operator ==(CadId left, CadId right)
		=> left.Value == right.Value;

	public static bool operator !=(CadId left, CadId right)
		=> !(left == right);
}

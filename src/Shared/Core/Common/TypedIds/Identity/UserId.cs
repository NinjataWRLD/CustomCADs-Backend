using System.Diagnostics.CodeAnalysis;

namespace CustomCADs.Shared.Core.Common.TypedIds.Identity;

public readonly struct UserId
{
	public UserId() : this(Guid.Empty) { }
	private UserId(Guid value)
	{
		Value = value;
	}

	public Guid Value { get; init; }

	public static UserId New() => new(Guid.NewGuid());
	public static UserId New(Guid id) => new(id);
	public static UserId? New(Guid? id) => id is null ? null : new(id.Value);
	public static UserId New(string? id) => id is null ? new() : new(Guid.Parse(id));

	public override bool Equals([NotNullWhen(true)] object? obj)
		=> obj is UserId id && this == id;

	public override int GetHashCode()
		=> Value.GetHashCode();

	public override string ToString()
		=> Value.ToString();

	public static bool operator ==(UserId left, UserId right)
		=> left.Value == right.Value;

	public static bool operator !=(UserId left, UserId right)
		=> !(left == right);
}

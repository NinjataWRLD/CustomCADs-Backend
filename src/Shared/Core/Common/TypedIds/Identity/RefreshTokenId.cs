using System.Diagnostics.CodeAnalysis;

namespace CustomCADs.Shared.Core.Common.TypedIds.Identity;

public readonly struct RefreshTokenId
{
	public RefreshTokenId() : this(Guid.Empty) { }
	private RefreshTokenId(Guid value)
	{
		Value = value;
	}

	public Guid Value { get; init; }

	public static RefreshTokenId New() => new(Guid.NewGuid());
	public static RefreshTokenId New(Guid id) => new(id);
	public static RefreshTokenId? New(Guid? id) => id is null ? null : new(id.Value);
	public static RefreshTokenId New(string? id) => id is null ? new() : new(Guid.Parse(id));

	public override bool Equals([NotNullWhen(true)] object? obj)
		=> obj is RefreshTokenId id && this == id;

	public override int GetHashCode()
		=> Value.GetHashCode();

	public override string ToString()
		=> Value.ToString();

	public static bool operator ==(RefreshTokenId left, RefreshTokenId right)
		=> left.Value == right.Value;

	public static bool operator !=(RefreshTokenId left, RefreshTokenId right)
		=> !(left == right);
}

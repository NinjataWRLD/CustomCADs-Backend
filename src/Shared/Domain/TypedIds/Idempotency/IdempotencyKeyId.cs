using System.Diagnostics.CodeAnalysis;

namespace CustomCADs.Shared.Domain.TypedIds.Idempotency;

public readonly struct IdempotencyKeyId
{
	public IdempotencyKeyId() : this(Guid.Empty) { }
	private IdempotencyKeyId(Guid value)
	{
		Value = value;
	}

	public Guid Value { get; init; }

	public static IdempotencyKeyId New() => new(Guid.NewGuid());
	public static IdempotencyKeyId New(Guid id) => new(id);

	public override bool Equals([NotNullWhen(true)] object? obj)
		=> obj is IdempotencyKeyId id && this == id;

	public override int GetHashCode()
		=> Value.GetHashCode();

	public override string ToString()
		=> Value.ToString();

	public static bool operator ==(IdempotencyKeyId left, IdempotencyKeyId right)
		=> left.Value == right.Value;

	public static bool operator !=(IdempotencyKeyId left, IdempotencyKeyId right)
		=> !(left == right);
}

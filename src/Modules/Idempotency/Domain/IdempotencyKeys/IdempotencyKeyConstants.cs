namespace CustomCADs.Idempotency.Domain.IdempotencyKeys;

public static class IdempotencyKeyConstants
{
	public const int MinStatusCode = 100;
	public const int MaxStatusCode = 999;

	public const int ClearIdempotencyKeysIntervalHours = 24;
	public const int ClearIdempotencyKeysBeforeHours = 24;
}

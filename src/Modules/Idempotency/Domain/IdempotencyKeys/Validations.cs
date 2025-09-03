namespace CustomCADs.Idempotency.Domain.IdempotencyKeys;

using static IdempotencyKeyConstants;

public static class Validations
{
	public static IdempotencyKey ValidateIdempotencyKey(this IdempotencyKey idempotencyKey)
		=> idempotencyKey
			.ThrowIfNull(
				expression: (x) => x.Id,
				predicate: (x) => x.IsEmpty()
			);

	public static IdempotencyKey ValidateRequestHash(this IdempotencyKey idempotencyKey)
		=> idempotencyKey
			.ThrowIfNull(
				expression: (x) => x.RequestHash,
				predicate: string.IsNullOrWhiteSpace
			);

	public static IdempotencyKey ValidateResponseBody(this IdempotencyKey idempotencyKey)
		=> idempotencyKey
			.ThrowIfNull(
				expression: (x) => x.ResponseBody,
				predicate: (x) => x is null
			);

	public static IdempotencyKey ValidateStatusCode(this IdempotencyKey idempotencyKey)
		=> idempotencyKey
			.ThrowIfNull(
				expression: (x) => x.StatusCode,
				predicate: (x) => x is null
			)
			.ThrowIfInvalidRange(
				expression: (x) => (int)x.StatusCode!,
				range: (MinStatusCode, MaxStatusCode),
				property: nameof(idempotencyKey.StatusCode)
			);
}

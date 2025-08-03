using CustomCADs.Idempotency.Domain.IdempotencyKeys;
using CustomCADs.Shared.Core.Common.TypedIds.Idempotency;

namespace CustomCADs.UnitTests.Idempotency.Data;

using static IdempotencyKeyConstants;

public static class IdempotencyKeysData
{
	public const string ValidRequestHash = "abcdefghijk";
	public const string InvalidRequestHash = "";

	public const string ValidResponseBody = "abcdefghijk";
	public const string InvalidResponseBody = null;

	public const int MinValidStatusCode = MinStatusCode + 1;
	public const int MaxValidStatusCode = MaxStatusCode - 1;
	public const int MinInvalidStatusCode = MinStatusCode - 1;
	public const int MaxInvalidStatusCode = MaxStatusCode + 1;

	public static readonly IdempotencyKeyId ValidId = IdempotencyKeyId.New();
	public static readonly IdempotencyKeyId InvalidId = IdempotencyKeyId.New(Guid.Empty);
}

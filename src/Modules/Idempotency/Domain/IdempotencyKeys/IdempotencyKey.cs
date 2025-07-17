using CustomCADs.Shared.Core.Bases.Entities;

namespace CustomCADs.Idempotency.Domain.IdempotencyKeys;

public class IdempotencyKey : BaseAggregateRoot
{
	private IdempotencyKey() { }
	private IdempotencyKey(IdempotencyKeyId id, string hash, string body, int status)
	{
		Id = id;
		RequestHash = hash;
		ResponseBody = body;
		StatusCode = status;
		CreatedAt = DateTimeOffset.UtcNow;
	}

	public IdempotencyKeyId Id { get; init; }
	public string RequestHash { get; init; } = string.Empty;
	public string ResponseBody { get; init; } = string.Empty;
	public int StatusCode { get; init; }
	public DateTimeOffset CreatedAt { get; init; }

	public static IdempotencyKey Create(IdempotencyKeyId id, string hash, string body, int status)
		=> new IdempotencyKey(id, hash, body, status)
			.ValidateIdempotencyKey()
			.ValidateRequestHash()
			.ValidateResponseBody()
			.ValidateStatusCode();
}

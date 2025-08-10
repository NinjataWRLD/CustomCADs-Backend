using CustomCADs.Shared.Domain.Bases.Entities;

namespace CustomCADs.Idempotency.Domain.IdempotencyKeys;

public class IdempotencyKey : BaseAggregateRoot
{
	private IdempotencyKey() { }
	private IdempotencyKey(IdempotencyKeyId id, string hash)
	{
		Id = id;
		RequestHash = hash;
		CreatedAt = DateTimeOffset.UtcNow;
	}

	public IdempotencyKeyId Id { get; init; }
	public string RequestHash { get; init; } = string.Empty;
	public string? ResponseBody { get; private set; }
	public int? StatusCode { get; private set; }
	public DateTimeOffset CreatedAt { get; init; }

	public static IdempotencyKey Create(IdempotencyKeyId id, string hash)
		=> new IdempotencyKey(id, hash)
			.ValidateIdempotencyKey()
			.ValidateRequestHash();

	public IdempotencyKey SetResponseBody(string body)
	{
		ResponseBody = body;
		this.ValidateResponseBody();

		return this;
	}

	public IdempotencyKey SetStatusCode(int status)
	{
		StatusCode = status;
		this.ValidateStatusCode();

		return this;
	}
}

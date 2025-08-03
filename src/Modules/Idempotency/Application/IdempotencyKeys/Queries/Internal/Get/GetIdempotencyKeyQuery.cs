namespace CustomCADs.Idempotency.Application.IdempotencyKeys.Queries.Internal.Get;

public record GetIdempotencyKeyQuery(
	Guid IdempotencyKey,
	string RequestHash
) : IQuery<GetIdempotencyKeyDto?>;

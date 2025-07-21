namespace CustomCADs.Idempotency.Application.IdempotencyKeys.Queries.Internal.Get;

public record GetIdempotencyKeyDto(
	string ResponseBody,
	int StatusCode
);

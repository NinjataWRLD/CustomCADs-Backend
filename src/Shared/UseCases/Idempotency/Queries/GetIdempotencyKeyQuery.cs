namespace CustomCADs.Shared.UseCases.Idempotency.Queries;

public record GetIdempotencyKeyQuery(
	Guid IdempotencyKey,
	string RequestHash
) : IQuery<GetIdempotencyKeyDto>;

public record GetIdempotencyKeyDto(
	string ResponseBody,
	int StatusCode
);

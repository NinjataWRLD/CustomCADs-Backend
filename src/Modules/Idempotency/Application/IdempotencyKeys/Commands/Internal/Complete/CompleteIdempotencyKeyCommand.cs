namespace CustomCADs.Idempotency.Application.IdempotencyKeys.Commands.Internal.Complete;

public record CompleteIdempotencyKeyCommand(
	IdempotencyKeyId Id,
	string RequestHash,
	string ResponseBody,
	int StatusCode
) : ICommand;

namespace CustomCADs.Idempotency.Application.IdempotencyKeys.Commands.Internal.Delete;

public record DeleteIdempotencyKeyCommand(
	Guid IdempotencyKey,
	string RequestHash
) : ICommand;

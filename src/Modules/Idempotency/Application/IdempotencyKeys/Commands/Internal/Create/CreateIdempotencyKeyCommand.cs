namespace CustomCADs.Idempotency.Application.IdempotencyKeys.Commands.Internal.Create;

public record CreateIdempotencyKeyCommand(
	Guid IdempotencyKey,
	string RequestHash
) : ICommand<IdempotencyKeyId>;

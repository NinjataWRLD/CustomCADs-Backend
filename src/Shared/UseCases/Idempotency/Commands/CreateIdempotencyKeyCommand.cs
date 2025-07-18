using CustomCADs.Shared.Core.Common.TypedIds.Idempotency;

namespace CustomCADs.Shared.UseCases.Idempotency.Commands;

public record CreateIdempotencyKeyCommand(
	Guid IdempotencyKey,
	string RequestHash,
	string ResponseBody,
	int StatusCode
) : ICommand<IdempotencyKeyId>;

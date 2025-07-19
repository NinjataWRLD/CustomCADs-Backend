using CustomCADs.Idempotency.Domain.Repositories;
using CustomCADs.Idempotency.Domain.Repositories.Reads;

namespace CustomCADs.Idempotency.Application.IdempotencyKeys.Commands.Internal.Complete;

public class CompleteIdempotencyKeyHandler(IIdempotencyKeyReads reads, IUnitOfWork uow)
	: ICommandHandler<CompleteIdempotencyKeyCommand>
{
	public async Task Handle(CompleteIdempotencyKeyCommand req, CancellationToken ct = default)
	{
		IdempotencyKey idempotencyKey = await reads.SingleByIdAsync(req.Id, req.RequestHash, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<IdempotencyKey>.ById(new { IdempotencyKey = req.Id, req.RequestHash });

		idempotencyKey.SetResponseBody(req.ResponseBody);
		idempotencyKey.SetStatusCode(req.StatusCode);

		await uow.SaveChangesAsync(ct).ConfigureAwait(false);
	}
}

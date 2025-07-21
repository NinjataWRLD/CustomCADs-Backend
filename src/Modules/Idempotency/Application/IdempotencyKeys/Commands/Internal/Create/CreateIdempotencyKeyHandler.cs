using CustomCADs.Idempotency.Domain.Repositories;

namespace CustomCADs.Idempotency.Application.IdempotencyKeys.Commands.Internal.Create;

public class CreateIdempotencyKeyHandler(IWrites<IdempotencyKey> writes, IUnitOfWork uow)
	: ICommandHandler<CreateIdempotencyKeyCommand, IdempotencyKeyId>
{
	public async Task<IdempotencyKeyId> Handle(CreateIdempotencyKeyCommand req, CancellationToken ct = default)
	{
		IdempotencyKey idempotencyKey = await writes.AddAsync(
			entity: IdempotencyKey.Create(
				id: IdempotencyKeyId.New(req.IdempotencyKey),
				hash: req.RequestHash
			),
			ct
		).ConfigureAwait(false);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		return idempotencyKey.Id;
	}
}

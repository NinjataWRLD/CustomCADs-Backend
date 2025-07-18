using CustomCADs.Idempotency.Domain.Repositories.Reads;
using CustomCADs.Shared.UseCases.Idempotency.Queries;

namespace CustomCADs.Idempotency.Application.IdempotencyKeys.Queries.Shared.Get;

public class GetIdempotencyKeyHandler(IIdempotencyKeyReads reads)
	: IQueryHandler<GetIdempotencyKeyQuery, GetIdempotencyKeyDto>
{
	public async Task<GetIdempotencyKeyDto> Handle(GetIdempotencyKeyQuery req, CancellationToken ct = default)
	{
		IdempotencyKeyId id = IdempotencyKeyId.New(req.IdempotencyKey);

		IdempotencyKey idempotencyKey = await reads.SingleByIdAsync(id, req.RequestHash, track: false, ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<IdempotencyKey>.ById(id);

		return new(idempotencyKey.ResponseBody, idempotencyKey.StatusCode);
	}
}

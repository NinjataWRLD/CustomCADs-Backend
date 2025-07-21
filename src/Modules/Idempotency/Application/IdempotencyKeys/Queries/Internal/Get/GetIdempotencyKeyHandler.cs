using CustomCADs.Idempotency.Domain.Repositories.Reads;

namespace CustomCADs.Idempotency.Application.IdempotencyKeys.Queries.Internal.Get;

public class GetIdempotencyKeyHandler(IIdempotencyKeyReads reads)
	: IQueryHandler<GetIdempotencyKeyQuery, GetIdempotencyKeyDto?>
{
	public async Task<GetIdempotencyKeyDto?> Handle(GetIdempotencyKeyQuery req, CancellationToken ct = default)
	{
		IdempotencyKey idempotencyKey = await reads.SingleByIdAsync(
			id: IdempotencyKeyId.New(req.IdempotencyKey),
			requestHash: req.RequestHash,
			track: false,
			ct: ct
		).ConfigureAwait(false)
			?? throw CustomNotFoundException<IdempotencyKey>.ById(new { req.IdempotencyKey, req.RequestHash });

		if (string.IsNullOrWhiteSpace(idempotencyKey.ResponseBody) || !idempotencyKey.StatusCode.HasValue)
		{
			return null;
		}

		return new(idempotencyKey.ResponseBody, idempotencyKey.StatusCode.Value);
	}
}

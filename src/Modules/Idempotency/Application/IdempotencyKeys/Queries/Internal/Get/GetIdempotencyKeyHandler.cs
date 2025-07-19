using CustomCADs.Idempotency.Domain.Repositories.Reads;

namespace CustomCADs.Idempotency.Application.IdempotencyKeys.Queries.Internal.Get;

public class GetIdempotencyKeyHandler(IIdempotencyKeyReads reads)
	: IQueryHandler<GetIdempotencyKeyQuery, GetIdempotencyKeyDto?>
{
	public async Task<GetIdempotencyKeyDto?> Handle(GetIdempotencyKeyQuery req, CancellationToken ct = default)
	{
		IdempotencyKey idempotencyKey = await reads.SingleByIdAsync(
			IdempotencyKeyId.New(req.IdempotencyKey),
			req.RequestHash,
			track: false,
			ct
		).ConfigureAwait(false)
			?? throw CustomNotFoundException<IdempotencyKey>.ById(new { req.IdempotencyKey, req.RequestHash });

		if (string.IsNullOrWhiteSpace(idempotencyKey.ResponseBody) || !idempotencyKey.StatusCode.HasValue)
		{
			return null;
		}

		return new(idempotencyKey.ResponseBody, idempotencyKey.StatusCode.Value);
	}
}

using CustomCADs.Idempotency.Domain.Repositories;
using Quartz;

namespace CustomCADs.Idempotency.Application.IdempotencyKeys.BackgroundJobs;

using static IdempotencyKeyConstants;

public class ClearIdempotencyKeysJob(IUnitOfWork uow) : IJob
{
	public async Task Execute(IJobExecutionContext context)
	{
		await uow.ClearIdempotencyKeysAsync(
			before: DateTimeOffset.UtcNow.AddHours(-ClearIdempotencyKeysBeforeHours),
			ct: context.CancellationToken
		).ConfigureAwait(false);
	}
}

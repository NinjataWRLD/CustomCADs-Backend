using CustomCADs.Catalog.Domain.Repositories;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using Quartz;

namespace CustomCADs.Catalog.Application.Products.BackgroundJobs;

using static Constants;

public class ClearTagsJob(IProductReads reads, IUnitOfWork uow) : IJob
{
	public async Task Execute(IJobExecutionContext context)
	{
		var ct = context.CancellationToken;

		ProductId[] ids = await reads.AllAsync(
			before: DateTimeOffset.UtcNow.AddDays(-7),
			after: null,
			ct: ct
		).ConfigureAwait(false);
		await uow.ClearProductTagsAsync(ids, Tags.New, ct).ConfigureAwait(false);
	}
}

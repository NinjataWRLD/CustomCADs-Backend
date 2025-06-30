using CustomCADs.Catalog.Domain.Repositories;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using Quartz;

namespace CustomCADs.Catalog.Application.Products.BackgroundJobs;

using static Constants;
using static ProductConstants;

public class ClearTagsJob(IProductReads reads, IUnitOfWork uow) : IJob
{
	public async Task Execute(IJobExecutionContext context)
	{
		var ct = context.CancellationToken;

		Product? product = await reads.OldestByTagAsync(Tags.New, ct).ConfigureAwait(false);
		if (product is null)
		{
			return; // no products with such tag
		}

		ProductId[] ids = await reads.AllAsync(
			before: DateTimeOffset.UtcNow.AddDays(-ClearTagsBeforeDays),
			after: product.UploadedAt,
			ct: ct
		).ConfigureAwait(false);
		await uow.ClearProductTagsAsync(ids, Tags.New, ct).ConfigureAwait(false);
	}
}

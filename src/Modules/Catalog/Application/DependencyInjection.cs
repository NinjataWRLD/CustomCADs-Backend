using CustomCADs.Catalog.Application.Products.BackgroundJobs;
using Quartz;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

using static ProductConstants;

public static partial class DependencyInjection
{
	public static IServiceCollection AddCatalogBackgroundJobs(this IServiceCollection services)
	{
		services.AddQuartz(q =>
		{
			q.AddTrigger(opts => opts
				.ForJob(q.AddJob<ClearTagsJob>())
				.WithSimpleSchedule(schedule =>
					schedule
						.WithInterval(TimeSpan.FromDays(ClearTagsIntervalDays))
						.RepeatForever()
				));
		});

		return services;
	}
}

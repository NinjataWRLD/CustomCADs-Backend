using CustomCADs.Catalog.Application.Products.BackgroundJobs;
using Quartz;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

using static ProductConstants;

public static class DependencyInjection
{
	public static IServiceCollection AddBackgroundJobs(this IServiceCollection services)
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

	private static JobKey AddJob<TJob>(this IServiceCollectionQuartzConfigurator q, string? name = null)
		where TJob : IJob
	{
		JobKey key = new(name ?? typeof(TJob).Name);
		q.AddJob<TJob>(opts => opts.WithIdentity(key));
		return key;
	}
}

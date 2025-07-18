using CustomCADs.Idempotency.Application.IdempotencyKeys.BackgroundJobs;
using Quartz;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

using static IdempotencyKeyConstants;

public static class DependencyInjection
{
	public static IServiceCollection AddIdempotencyBackgroundJobs(this IServiceCollection services)
	{
		services.AddQuartz(q =>
		{
			q.AddTrigger(opts => opts
				.ForJob(q.AddJob<ClearIdempotencyKeysJob>())
				.WithSimpleSchedule(schedule =>
					schedule
						.WithInterval(TimeSpan.FromHours(ClearIdempotencyKeysIntervalHours))
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

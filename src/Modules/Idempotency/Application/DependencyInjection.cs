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
}

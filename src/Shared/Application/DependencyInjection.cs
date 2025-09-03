using CustomCADs.Shared.Application.UseCases.Common.Currencies.BackgroundJobs;
using Quartz;
using System.Runtime.InteropServices;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
	public static IServiceCollection AddSharedBackgroundJobs(this IServiceCollection services)
	{
		services.AddQuartz(q =>
		{
			TimeZoneInfo cet = TimeZoneInfo.FindSystemTimeZoneById(
				id: RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
					? "Central European Standard Time"
					: "Europe/Berlin"
			);

			q.AddTrigger(opts => opts
				.ForJob(q.AddJob<UpdateExchangeRatesCacheBackgroundJob>())
				.WithSchedule(
					CronScheduleBuilder
						.DailyAtHourAndMinute(16, 30)
						.InTimeZone(cet)
				));
		});

		return services;
	}

	public static JobKey AddJob<TJob>(this IServiceCollectionQuartzConfigurator q, string? name = null)
		where TJob : IJob
	{
		JobKey key = new(name ?? typeof(TJob).Name);
		q.AddJob<TJob>(opts => opts.WithIdentity(key));
		return key;
	}
}

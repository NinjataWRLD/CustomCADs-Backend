using Polly;

namespace CustomCADs.Shared.Infrastructure;

public static class ResilienceExtensions
{
	public static IAsyncPolicy AsyncRetry(
		this PolicyBuilder builder,
		int retryCount = 3,
		Func<int, TimeSpan>? sleepDurationProvider = null
	) => builder.WaitAndRetryAsync(
				retryCount,
				sleepDurationProvider ?? (
					(attempt) => TimeSpan.FromSeconds(Math.Pow(2, attempt))
				)
			);

	public static IAsyncPolicy AsyncCircuitBreak(
		this PolicyBuilder builder,
		int exceptionsAllowedBeforeBreaking = 2,
		TimeSpan? durationOfBreak = null
	) => builder.CircuitBreakerAsync(
				exceptionsAllowedBeforeBreaking,
				durationOfBreak ?? TimeSpan.FromSeconds(30)
			);
}

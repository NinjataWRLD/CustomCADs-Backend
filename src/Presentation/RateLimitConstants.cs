using System.Threading.RateLimiting;

namespace CustomCADs.Presentation;

internal static class RateLimitConstants
{
	internal static class Anonymous
	{
		internal const int GlobalLimit = 500;
		internal static readonly TimeSpan Window = TimeSpan.FromMinutes(1);
		internal const int QueueLimit = 10;
		internal const QueueProcessingOrder QueueOrder = QueueProcessingOrder.OldestFirst;
		internal const bool AutoReplenish = true;
	}

	internal static class Authenticated
	{
		internal const int BurstLimit = 200;
		internal static readonly TimeSpan Period = TimeSpan.FromMinutes(1);
		internal const int ReplenishedTokens = 100;
		internal const int QueueLimit = 20;
		internal const QueueProcessingOrder QueueOrder = QueueProcessingOrder.OldestFirst;
		internal const bool AutoReplenish = true;
	}
}

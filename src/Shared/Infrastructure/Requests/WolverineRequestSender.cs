using CustomCADs.Shared.Application.Abstractions.Cache;
using CustomCADs.Shared.Application.Abstractions.Requests.Attributes;
using CustomCADs.Shared.Application.Abstractions.Requests.Commands;
using CustomCADs.Shared.Application.Abstractions.Requests.Queries;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using System.Reflection;

namespace CustomCADs.Shared.Infrastructure.Requests;

public class WolverineRequestSender(Wolverine.IMessageBus bus, ICacheService cache) : IRequestSender
{
	private const string QueryGroup = "queries";

	public async Task SendCommandAsync(ICommand command, CancellationToken ct = default)
		=> await bus.InvokeAsync(command, ct).ConfigureAwait(false);

	public async Task<TResponse> SendCommandAsync<TResponse>(ICommand<TResponse> command, CancellationToken ct = default)
		=> await bus.InvokeAsync<TResponse>(command, ct).ConfigureAwait(false);

	public async Task<TResponse> SendQueryAsync<TResponse>(IQuery<TResponse> query, CancellationToken ct = default)
	{
		async Task<TResponse> InvokeAsync()
			=> await bus.InvokeAsync<TResponse>(query, ct).ConfigureAwait(false);

		AddRequestCachingAttribute[] requestCachingAttributes = [..
			query.GetType().GetCustomAttributes<AddRequestCachingAttribute>()
		];
		if (requestCachingAttributes.Length != 0)
		{
			TResponse? entry = await cache.GetOrCreateAsync(
				key: query.Hash(QueryGroup),
				factory: InvokeAsync,
				expiration: requestCachingAttributes.CaclulateExpiration()
			).ConfigureAwait(false);

			// entry response can only ever be null
			// if a previous call returned null and was saved to cache
			// thus, null is a valid response
			return entry!;
		}

		return await InvokeAsync().ConfigureAwait(false);
	}
}

using CustomCADs.Shared.Abstractions.Requests.Commands;
using CustomCADs.Shared.Abstractions.Requests.Queries;
using CustomCADs.Shared.Abstractions.Requests.Sender;

namespace CustomCADs.Shared.Infrastructure.Requests;

public class WolverineRequestSender(Wolverine.IMessageBus bus) : IRequestSender
{
	public async Task SendCommandAsync(ICommand command, CancellationToken ct = default)
		=> await bus.InvokeAsync(command, ct).ConfigureAwait(false);

	public async Task<TResponse> SendCommandAsync<TResponse>(ICommand<TResponse> command, CancellationToken ct = default)
		=> await bus.InvokeAsync<TResponse>(command, ct).ConfigureAwait(false);

	public async Task<TResponse> SendQueryAsync<TResponse>(IQuery<TResponse> query, CancellationToken ct = default)
		=> await bus.InvokeAsync<TResponse>(query, ct).ConfigureAwait(false);
}

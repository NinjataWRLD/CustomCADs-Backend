using CustomCADs.Shared.Abstractions.Requests.Commands;
using CustomCADs.Shared.Abstractions.Requests.Queries;

namespace CustomCADs.Shared.Abstractions.Requests.Sender;

public interface IRequestSender
{
    Task SendCommandAsync(ICommand command, CancellationToken ct = default);
    Task<TResponse> SendCommandAsync<TResponse>(ICommand<TResponse> command, CancellationToken ct = default);
    Task<TResponse> SendQueryAsync<TResponse>(IQuery<TResponse> query, CancellationToken ct = default);
}

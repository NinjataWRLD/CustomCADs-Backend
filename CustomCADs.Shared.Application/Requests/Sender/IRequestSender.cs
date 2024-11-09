using CustomCADs.Shared.Application.Requests.Commands;
using CustomCADs.Shared.Application.Requests.Queries;

namespace CustomCADs.Shared.Application.Requests.Sender;

public interface IRequestSender
{
    Task SendCommandAsync(ICommand command, CancellationToken ct = default);
    Task<TResponse> SendCommandAsync<TResponse>(ICommand<TResponse> command, CancellationToken ct = default);

    Task SendQueryAsync(IQuery query, CancellationToken ct = default);
    Task<TResponse> SendQueryAsync<TResponse>(IQuery<TResponse> query, CancellationToken ct = default);
}

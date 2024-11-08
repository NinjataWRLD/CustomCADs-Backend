using CustomCADs.Shared.Application.Requests.Commands;
using CustomCADs.Shared.Application.Requests.Queries;

namespace CustomCADs.Shared.Application.Requests.Sender;

public interface IRequestSender
{
    Task Send(ICommand command, CancellationToken ct = default);
    Task<TResponse> Send<TResponse>(ICommand<TResponse> command, CancellationToken ct = default);

    Task Send(IQuery query, CancellationToken ct = default);
    Task<TResponse> Send<TResponse>(IQuery<TResponse> query, CancellationToken ct = default);
}

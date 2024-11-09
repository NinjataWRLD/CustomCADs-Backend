using CustomCADs.Shared.Application.Requests.Commands;
using CustomCADs.Shared.Application.Requests.Queries;
using MediatR;

namespace CustomCADs.Shared.Application.Requests.Sender;

public class RequestSender(IMediator mediator) : IRequestSender
{
    public async Task SendCommandAsync(ICommand command, CancellationToken ct = default)
        => await mediator.Send(command, ct).ConfigureAwait(false);

    public async Task<TResponse> SendCommandAsync<TResponse>(ICommand<TResponse> command, CancellationToken ct = default)
        => await mediator.Send(command, ct).ConfigureAwait(false);

    public async Task SendQueryAsync(IQuery query, CancellationToken ct = default)
        => await mediator.Send(query, ct).ConfigureAwait(false);

    public async Task<TResponse> SendQueryAsync<TResponse>(IQuery<TResponse> query, CancellationToken ct = default)
        => await mediator.Send(query, ct).ConfigureAwait(false);
}

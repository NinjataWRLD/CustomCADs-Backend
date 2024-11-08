using CustomCADs.Shared.Application.Requests.Commands;
using CustomCADs.Shared.Application.Requests.Queries;
using MediatR;

namespace CustomCADs.Shared.Application.Requests.Sender;

public class RequestSender(IMediator mediator) : IRequestSender
{
    public async Task Send(ICommand command, CancellationToken ct = default)
        => await mediator.Send(command, ct).ConfigureAwait(false);

    public async Task<TResponse> Send<TResponse>(ICommand<TResponse> command, CancellationToken ct = default)
        => await mediator.Send(command, ct).ConfigureAwait(false);

    public async Task Send(IQuery query, CancellationToken ct = default)
        => await mediator.Send(query, ct).ConfigureAwait(false);

    public async Task<TResponse> Send<TResponse>(IQuery<TResponse> query, CancellationToken ct = default)
        => await mediator.Send(query, ct).ConfigureAwait(false);
}
